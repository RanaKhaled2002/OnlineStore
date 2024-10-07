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
        public ProductSpecification() 
        {
            ApplyIncludes();
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
