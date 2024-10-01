﻿using AutoMapper;
using OnlineStore.Core.DTOs.Products;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Services.Contract;
using OnlineStore.Core.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _UnitOfWork.Repository<Product, int>().GetAllAsync());
        }

        public async Task<ProductDto> GetProductById(int id)
        {
          return _mapper.Map<ProductDto>( await _UnitOfWork.Repository<Product, int>().GetByIdAsync(id));
        }

        public async Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<BrandTypeDto>>(await _UnitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }

        public async Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync()
        {
           return _mapper.Map<IEnumerable<BrandTypeDto>>( await _UnitOfWork.Repository<ProductType,int>().GetAllAsync());
        }

       
    }
}
