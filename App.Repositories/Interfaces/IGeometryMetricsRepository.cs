using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Interfaces
{
    public interface IGeometryMetricsRepository : IGenericRepository<GeometryMetricsEntity>
    {
        Task<GeometryMetricsEntity?> GetByGeometryIdAsync(int geometryId);
    }
}
