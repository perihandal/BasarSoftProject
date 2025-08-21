using System;
using App.Repositories.Interfaces;

namespace App.Repositories
{
    public class UnitOfWork(EFAppDbContext context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync() => context.SaveChangesAsync(); // await koymama gerek yok zaten arka planda o şekilde çalışcak

    }
}
