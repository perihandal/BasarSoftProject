using App.Core.Entities;
using App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Repositories
{
    public class GeometryMetricsRepository(EFAppDbContext context) : GenericRepository<GeometryMetricsEntity>(context), IGeometryMetricsRepository
    {
        public async Task<GeometryMetricsEntity?> GetByGeometryIdAsync(int geometryId)
        {
            return await context.Set<GeometryMetricsEntity>()
            .AsNoTracking()
                .FirstOrDefaultAsync(g => g.GeometryId == geometryId);
        }
    }
}
