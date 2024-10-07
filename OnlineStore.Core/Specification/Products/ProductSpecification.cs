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
        public ProductSpecification(ProductSpecParams productSpec ) : 
            base(P => (!productSpec.brandId.HasValue || productSpec.brandId == P.BrandId) && (!productSpec.typeId.HasValue ||productSpec.typeId == P.TypeId))
        {
            if(!string.IsNullOrEmpty(productSpec.sort))
            {
                switch (productSpec.sort)
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
            AddPagination(productSpec.pageSize * (productSpec.pageIndex - 1), productSpec.pageSize);
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
