using OnlineStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Specification.Products
{
    public class ProductWithCountSpecification : BaseSpecification<Product,int>
    {
        public ProductWithCountSpecification(ProductSpecParams productSpec) :
            base(P => (!productSpec.brandId.HasValue || productSpec.brandId == P.BrandId) && (!productSpec.typeId.HasValue || productSpec.typeId == P.TypeId))
        {
         
        }
    }
}
