using App.Core.DTOs;
using App.Services.Responses;

public interface IGeometryInfoService
{
    Task<Response<bool>> DeleteAsync(int geometryId);
    Task<Response<GeometryInfoDto>> GetByGeometryIdAsync(int geometryId);
    Task<Response<GeometryInfoDto>> UpdateAsync(int geometryId, GeometryInfoDto dto);
    Task<Response<GeometryInfoDto>> AddAsync(GeometryInfoDto dto, int geometryId);
}