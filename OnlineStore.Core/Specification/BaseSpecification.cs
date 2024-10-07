using OnlineStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specification
{
    public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get; set; }
        public Expression<Func<TEntity, object>> OrderByDesc { get; set; }
        public int Skip { get ; set; }
        public int Take { get ; set ; }
        public bool IsPagination { get; set; }

        public BaseSpecification() { }

        public BaseSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
        }

        public void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy = expression;
        }

        public void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc = expression;
        }

        public void AddPagination(int skip,int take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;
        }
    }
}
