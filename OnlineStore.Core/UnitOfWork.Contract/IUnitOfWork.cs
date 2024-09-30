using OnlineStore.Core.Entities;
using OnlineStore.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.UnitOfWork.Contract
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        IGenericRepository<TEntity,TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
