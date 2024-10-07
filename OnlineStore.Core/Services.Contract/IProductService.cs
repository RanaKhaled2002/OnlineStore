using OnlineStore.Core.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services.Contract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort, int? brandId,int? typeId,int?pageIndex,int?pageSize);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync();
        Task<ProductDto> GetProductById(int id);
    }
}
