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
    public class GeometryRepository(EFAppDbContext context) : GenericRepository<GeometryEntity>(context), IGeometryRepository
    {
        
        public async Task<GeometryEntity?> GetByIdAsync(int id)
        {
            return await context.Set<GeometryEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<GeometryEntity>> GetAllAsync()
        {
            return await context.Set<GeometryEntity>()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
