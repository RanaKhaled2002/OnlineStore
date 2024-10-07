using AutoMapper;
using Microsoft.Extensions.Configuration;
using OnlineStore.Core.DTOs.Products;
using OnlineStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDto>()
                .ForMember(P=>P.BrandName, options => options.MapFrom(B=>B.Brand.Name))
                .ForMember(P=>P.TypeName, options => options.MapFrom(T=>T.Type.Name))
                .ForMember(P=>P.PictureUrl, options => options.MapFrom(S => $"{configuration["BaseUrl"]}/{S.PictureUrl}"));

            CreateMap<ProductBrand, BrandTypeDto>();
            CreateMap<ProductType, BrandTypeDto>();
        }
    }
}
