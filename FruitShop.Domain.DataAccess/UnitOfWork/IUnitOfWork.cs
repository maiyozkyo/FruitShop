using Shoping.Data_Access.Repo;

namespace Shoping.Data_Access.DB.UnitOfWork
{
    interface IUnitOfWork: IDisposable 
    {
        public Task<int> SaveChangesAsync();
    }
}
