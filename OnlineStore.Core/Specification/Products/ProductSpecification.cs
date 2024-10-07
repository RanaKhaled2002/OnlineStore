using OnlineStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specification.Products
{
    public class ProductSpecification : BaseSpecification<Product,int>
    {
        public ProductSpecification(string? sort , int? brandId, int? typeId,int pageIndex,int pageSize ) : 
            base(P => (! brandId.HasValue || brandId== P.BrandId) && (!typeId.HasValue ||typeId == P.TypeId))
        {
            if(!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(P=>P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            ApplyIncludes();
            AddPagination(pageSize*(pageIndex-1),pageSize);
        }

        public ProductSpecification(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            Includes.Add(P=>P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
