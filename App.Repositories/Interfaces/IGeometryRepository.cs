using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Interfaces
{
    public interface IGeometryRepository:IGenericRepository<GeometryEntity>
    {
        Task<GeometryEntity?> GetByIdAsync(int id);
        Task<List<GeometryEntity>> GetAllAsync();
    }
}
