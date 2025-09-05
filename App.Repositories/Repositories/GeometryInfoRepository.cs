using App.Core.Entities;
using App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace App.Repositories.Repositories
{
    public class GeometryInfoRepository(EFAppDbContext context) : GenericRepository<GeometryInfoEntity>(context), IGeometryInfoRepository
    {
        public async Task<GeometryInfoEntity?> GetByGeometryIdAsync(int geometryId)
        {
            return await context.Set<GeometryInfoEntity>()
            .AsNoTracking()
                .FirstOrDefaultAsync(g => g.GeometryId == geometryId);
        }


    }
}




