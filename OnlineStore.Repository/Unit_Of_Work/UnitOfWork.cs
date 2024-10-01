using OnlineStore.Core.Entities;
using OnlineStore.Core.Repositories.Contract;
using OnlineStore.Core.UnitOfWork.Contract;
using OnlineStore.Repository.Data.Contexts;
using OnlineStore.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _Context;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _Context = context;
            _repositories = new Hashtable();
        }


        public async Task<int> CompleteAsync()
        {
            return await _Context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repoistory = new GenericRepository<TEntity, TKey>(_Context);

                _repositories.Add(type, repoistory);
            }
            return _repositories[type] as IGenericRepository<TEntity, TKey>;
        }
    }
}
