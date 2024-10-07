using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Repository.Specification_Evalutor
{
    public static class SpecificationEvalutor<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity,TKey> spec)
        {
            var Query = inputQuery;

            if(spec.Criteria is not null)
            {
                Query = Query.Where(spec.Criteria);
            }

            if(spec.OrderBy is not null)
            {
                Query = Query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDesc  is not null)
            {
                Query = Query.OrderByDescending(spec.OrderByDesc);
            }

            if(spec.IsPagination)
            {
                Query = Query.Skip(spec.Skip).Take(spec.Take);
            }

            Query = spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            return Query;
        }
    }
}
