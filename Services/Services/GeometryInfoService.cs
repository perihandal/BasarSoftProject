using App.Core.DTOs;
using App.Core.Entities;
using App.Repositories.Interfaces;
using App.Services.Responses;
using App.Services.Resources;
using System.Threading.Tasks;

namespace App.Services.Services
{
    public class GeometryInfoService : IGeometryInfoService
    {
        private readonly IGeometryInfoRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public GeometryInfoService(IGeometryInfoRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<GeometryInfoDto>> AddAsync(GeometryInfoDto dto, int geometryId)
        {
            var entity = new GeometryInfoEntity
            {
                GeometryId = geometryId,
                FullAddress = dto.FullAddress,
                Phone = dto.Phone,
                PhotoBase64 = dto.PhotoBase64,
                OpeningHours = dto.OpeningHours,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };

            await repository.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return Response<GeometryInfoDto>.Ok(dto, "Geometry info added successfully");
        }

        public async Task<Response<GeometryInfoDto>> UpdateAsync(int geometryId, GeometryInfoDto dto)
        {
            var entity = await repository.GetByGeometryIdAsync(geometryId);
            if (entity == null)
                return Response<GeometryInfoDto>.Fail("Geometry info not found");

            entity.FullAddress = dto.FullAddress;
            entity.Phone = dto.Phone;
            entity.PhotoBase64 = dto.PhotoBase64;
            entity.OpeningHours = dto.OpeningHours;
            entity.Description = dto.Description;
            entity.UpdatedAt = DateTime.UtcNow;


            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            return Response<GeometryInfoDto>.Ok(dto, "Geometry info updated successfully");
        }

        public async Task<Response<GeometryInfoDto>> GetByGeometryIdAsync(int geometryId)
        {
            var entity = await repository.GetByGeometryIdAsync(geometryId);
            if (entity == null)
                return Response<GeometryInfoDto>.Fail("Geometry info not found");

            var dto = new GeometryInfoDto
            {
                FullAddress = entity.FullAddress,
                Phone = entity.Phone,
                PhotoBase64 = entity.PhotoBase64,
                OpeningHours = entity.OpeningHours,
                Description = entity.Description
            };

            return Response<GeometryInfoDto>.Ok(dto);
        }

        public async Task<Response<bool>> DeleteAsync(int geometryId)
        {
            var entity = await repository.GetByGeometryIdAsync(geometryId);
            if (entity == null) return Response<bool>.Fail("Geometry info not found");

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();

            return Response<bool>.Ok(true, "Geometry info deleted successfully");
        }
    }
}
