using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Data_Access.Repo
{
    interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        IQueryable<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition);
        Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> condition);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void UpdateField(string field, dynamic value = null);
    }
}
