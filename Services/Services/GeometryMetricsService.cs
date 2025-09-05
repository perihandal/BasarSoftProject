using App.Core.DTOs;
using App.Core.Entities;
using App.Repositories.Interfaces;
using App.Services.Responses;
using App.Services.Resources;
using App.Services.Interfaces;
using NetTopologySuite.Geometries;

namespace App.Services.Services
{
    public class GeometryMetricsService : IGeometryMetricsService
    {
        private readonly IGeometryMetricsRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public GeometryMetricsService(IGeometryMetricsRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public GeometryMetricsEntity CalculateMetrics(Geometry geometry, int geometryId)
        {
            var metrics = new GeometryMetricsEntity
            {
                GeometryId = geometryId,
                Area = geometry is Polygon || geometry is MultiPolygon ? geometry.Area : null,
                Length = geometry.Length,
                Centroid = geometry.Centroid,
                BoundingBox = geometry.Envelope as Polygon
            };

            if (geometry is LineString line)
            {
                metrics.StartPoint = line.StartPoint;
                metrics.EndPoint = line.EndPoint;
            }

            return metrics;
        }
        public async Task<Response<GeometryMetricsDto>> AddAsync(GeometryMetricsEntity metrics)
        {
            await repository.AddAsync(metrics);
            await unitOfWork.SaveChangesAsync();
            return Response<GeometryMetricsDto>.Ok(new GeometryMetricsDto
            {
                GeometryId = metrics.GeometryId,
                Area = metrics.Area,
                Length = metrics.Length
            }, "Metrics added successfully");
        }

        public async Task<Response<GeometryMetricsDto>> UpdateAsync(GeometryMetricsEntity metrics)
        {
            repository.Update(metrics);
            await unitOfWork.SaveChangesAsync();
            return Response<GeometryMetricsDto>.Ok(new GeometryMetricsDto
            {
                GeometryId = metrics.GeometryId,
                Area = metrics.Area,
                Length = metrics.Length
            }, "Metrics updated successfully");
        }

        public async Task<Response<GeometryMetricsDto>> GetByGeometryIdAsync(int geometryId)
        {
            var entity = await repository.GetByGeometryIdAsync(geometryId);
            if (entity == null) return Response<GeometryMetricsDto>.Fail("Metrics not found");

            var dto = new GeometryMetricsDto
            {

                GeometryId = entity.GeometryId,
                Area = entity.Area,
                Length = entity.Length,
                StartPoint = entity.StartPoint,
                EndPoint = entity.EndPoint,
                Centroid = entity.Centroid,
                BoundingBox = entity.BoundingBox
            };

            return Response<GeometryMetricsDto>.Ok(dto);
        }

        public async Task<Response<bool>> DeleteAsync(int geometryId)
        {
            var entity = await repository.GetByGeometryIdAsync(geometryId);
            if (entity == null) return Response<bool>.Fail("Metrics not found");

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();
            return Response<bool>.Ok(true, "Metrics deleted successfully");
        }


    }
}
