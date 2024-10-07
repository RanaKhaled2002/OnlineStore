using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Repositories.Contract;
using OnlineStore.Core.Specification;
using OnlineStore.Repository.Data.Contexts;
using OnlineStore.Repository.Specification_Evalutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository.Repositories
{
    public class GenericRepository<TEntity,TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public readonly StoreDbContext _Context;

        public GenericRepository(StoreDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {
               return (IEnumerable<TEntity>) await _Context.Products.Include(P => P.Brand).Include(P => P.Type).ToListAsync();
            }
           return await _Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            if(typeof(TEntity) == typeof(Product))
            {
               return await _Context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
            }
            return await _Context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
           await _Context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _Context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _Context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdWithSpecAsync(ISpecification<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TKey> spec)
        {
            return SpecificationEvalutor<TEntity,TKey>.GetQuery(_Context.Set<TEntity>(), spec);
        }
    }
}
