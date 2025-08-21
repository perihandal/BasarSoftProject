using App.Core.DTOs;
using App.Services.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGeometryService
    {
        Task<Response<GeometryDto>> AddGeometryAsync(GeometryDto dto);
        Task<Response<GeometryDto>> GetByIdAsync(int id);
        Task<Response<List<GeometryDto>>> GetAllAsync();
        Task<Response<GeometryDto>> UpdateGeometryAsync(int id, GeometryDto dto);
        Task<Response<bool>> DeleteGeometryAsync(int id);
    }
}
