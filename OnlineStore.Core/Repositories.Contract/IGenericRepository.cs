using OnlineStore.Core.Entities;
using OnlineStore.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity,TKey> spec);
        Task<TEntity> GetByIdWithSpecAsync(ISpecification<TEntity, TKey> spec);
        Task<int> GetCountAsync(ISpecification<TEntity,TKey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
