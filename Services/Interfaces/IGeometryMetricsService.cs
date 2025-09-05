using App.Core.DTOs;
using App.Core.Entities;
using App.Services.Responses;
using NetTopologySuite.Geometries;


namespace App.Services.Interfaces
{
    public interface IGeometryMetricsService
    {
        GeometryMetricsEntity CalculateMetrics(Geometry geometry, int geometryId);
        Task<Response<GeometryMetricsDto>> AddAsync(GeometryMetricsEntity metrics);
        Task<Response<GeometryMetricsDto>> UpdateAsync(GeometryMetricsEntity metrics);

        Task<Response<GeometryMetricsDto>> GetByGeometryIdAsync(int geometryId);
        Task<Response<bool>> DeleteAsync(int geometryId);
        }
}
