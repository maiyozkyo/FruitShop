using Microsoft.EntityFrameworkCore;
using Shoping.Data_Access.Repo;

namespace Shoping.Data_Access.DB.UnitOfWork
{
    public class UnitOfWork<TEntity>: IUnitOfWork where TEntity : class
    {
        private readonly DbContext _dbContext;
        public readonly Repository<TEntity> Repository;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            Repository = new Repository<TEntity>(dbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
