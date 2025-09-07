using App.Core.DTOs;
using App.Core.Entities;
using App.Core.Validators;
using App.Repositories.Interfaces;
using App.Services.Responses;
using NetTopologySuite.IO;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Interfaces;
using App.Services.Resources;
using App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Services
{
    public class GeometryService : IGeometryService
    {
        private readonly IGeometryRepository repository;
        private readonly IGeometryMetricsService metricsService;
        private readonly IGeometryInfoService infoService;
        private readonly IUnitOfWork unitOfWork;
        private readonly GeometryValidator validator;

        public GeometryService(IGeometryRepository repository,
            IGeometryMetricsService metricsService,
            IGeometryInfoService infoService, IUnitOfWork unitOfWork, GeometryValidator validator)
        {
            this.repository = repository;
            this.metricsService = metricsService;
            this.infoService = infoService;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        public async Task<Response<GeometryDto>> AddGeometryAsync(GeometryDto dto)
        {
            var validationMessage = validator.Validate(dto);
            if (validationMessage != null)
                return Response<GeometryDto>.Fail(validationMessage);

            var entity = new GeometryEntity
            {
               
                Name = dto.Name,
                Wkt = dto.Wkt,
                Type = Enum.Parse<App.Core.Entities.GeometryType>(dto.Type, true),
                Geoloc = new WKTReader().Read(dto.Wkt)
            };

            await repository.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            var metrics = metricsService.CalculateMetrics(entity.Geoloc, entity.Id);
            await metricsService.AddAsync(metrics);

            // GeometryInfo ekle
            var infoDto = new GeometryInfoDto
            {
                
                FullAddress = dto.FullAddress,
                Phone = dto.Phone,
                PhotoBase64 = dto.PhotoBase64,
                Description = dto.Description,
                OpeningHours = dto.OpeningHours
            };
            await infoService.AddAsync(infoDto, entity.Id);


            return Response<GeometryDto>.Ok(dto, ErrorMessages.AddSuccess);
        }

        public async Task<Response<List<GeometryDto>>> PaginationAsync(int pageNumber , int pageSize)
        {
            var query = repository.GetAll() 
                .Include(g => g.GeometryInfo)
                .Include(g => g.GeometryMetrics);


            var geometries = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!geometries.Any())
                return Response<List<GeometryDto>>.Fail("No geometries found.");

            var geometryDtos = geometries.Select(e => new GeometryDto
            {
                Id = e.Id,
                Name = e.Name,
                Wkt = e.Wkt,
                Type = e.Type.ToString(),

                FullAddress = e.GeometryInfo?.FullAddress,
                Phone = e.GeometryInfo?.Phone,
                PhotoBase64 = e.GeometryInfo?.PhotoBase64,
                Description = e.GeometryInfo?.Description,
                OpeningHours = e.GeometryInfo?.OpeningHours,

                Area = e.GeometryMetrics?.Area,
                Length = e.GeometryMetrics?.Length,
                Centroid = e.GeometryMetrics?.Centroid,
                BoundingBox = e.GeometryMetrics?.BoundingBox,
                StartPoint = e.GeometryMetrics?.StartPoint,
                EndPoint = e.GeometryMetrics?.EndPoint
            }).ToList();

            return Response<List<GeometryDto>>.Ok(geometryDtos);
        }


        public async Task<Response<GeometryDto>> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
                return Response<GeometryDto>.Fail(ErrorMessages.NotFound);

            var dto = new GeometryDto
            {
                Name = entity.Name,
                Wkt = entity.Wkt,
                Type = entity.Type.ToString(),

                FullAddress = entity.GeometryInfo?.FullAddress,
                Phone = entity.GeometryInfo?.Phone,
                PhotoBase64 = entity.GeometryInfo?.PhotoBase64,
                Description = entity.GeometryInfo?.Description,
                OpeningHours = entity.GeometryInfo?.OpeningHours,

                Area = entity.GeometryMetrics?.Area,
                Length = entity.GeometryMetrics?.Length,
                Centroid = entity.GeometryMetrics?.Centroid,
                BoundingBox = entity.GeometryMetrics?.BoundingBox,
                StartPoint = entity.GeometryMetrics?.StartPoint,
                EndPoint = entity.GeometryMetrics?.EndPoint
            };

            return Response<GeometryDto>.Ok(dto);
        }

        public async Task<Response<List<GeometryDto>>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            if (!entities.Any())
                return Response<List<GeometryDto>>.Fail(ErrorMessages.EmptyList);

            var list = entities.Select(e => new GeometryDto
            {
                Id = e.Id,
                Name = e.Name,
                Wkt = e.Wkt,
                Type = e.Type.ToString(),

                // GeometryInfo
                FullAddress = e.GeometryInfo?.FullAddress ?? "",
                Phone = e.GeometryInfo?.Phone ?? "",
                PhotoBase64 = e.GeometryInfo?.PhotoBase64 ?? "",
                Description = e.GeometryInfo?.Description ?? "",
                OpeningHours = e.GeometryInfo?.OpeningHours ?? "",

                // GeometryMetrics
                Area = e.GeometryMetrics?.Area ?? 0,
                Length = e.GeometryMetrics?.Length ?? 0,
                Centroid = e.GeometryMetrics?.Centroid,
                BoundingBox = e.GeometryMetrics?.BoundingBox,
                StartPoint = e.GeometryMetrics?.StartPoint,
                EndPoint = e.GeometryMetrics?.EndPoint
            }).ToList();

            return Response<List<GeometryDto>>.Ok(list);
        }


        public async Task<Response<GeometryDto>> UpdateGeometryAsync(int id, GeometryDto dto)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
                return Response<GeometryDto>.Fail(ErrorMessages.NotFound);

            var validationMessage = validator.Validate(dto);
            if (validationMessage != null)
                return Response<GeometryDto>.Fail(validationMessage);

            entity.Name = dto.Name;
            entity.Wkt = dto.Wkt;
            entity.Type = Enum.Parse<Core.Entities.GeometryType>(dto.Type, true);
            entity.Geoloc = new WKTReader().Read(dto.Wkt);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            var metricsResponse = await metricsService.GetByGeometryIdAsync(entity.Id);
            if (metricsResponse.Data != null)
            {
                var updatedMetrics = metricsService.CalculateMetrics(entity.Geoloc, entity.Id);
                await metricsService.UpdateAsync(updatedMetrics);
            }

            // GeometryInfo güncelle
            await infoService.UpdateAsync(entity.Id, new GeometryInfoDto
            {
                FullAddress = dto.FullAddress,
                Phone = dto.Phone,
                PhotoBase64 = dto.PhotoBase64,
                Description = dto.Description,
                OpeningHours = dto.OpeningHours
            });

            return Response<GeometryDto>.Ok(dto, ErrorMessages.UpdateSuccess);
        }

        public async Task<Response<bool>> DeleteGeometryAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
                return Response<bool>.Fail(ErrorMessages.NotFound);

            await metricsService.DeleteAsync(id);
            await infoService.DeleteAsync(id);

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();


            return Response<bool>.Ok(true, ErrorMessages.DeleteSuccess);
        }

        public async Task<int> GetGeometryCountAsync()
        {
            return await repository.GetAll()
                .CountAsync();
        }
    }
}