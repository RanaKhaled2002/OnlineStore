﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using OnlineStore.Core.DTOs.Orders;
using OnlineStore.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile(IConfiguration configuration) 
        {
            CreateMap<Order, OrderReturnDto>()
                .ForMember(D=>D.DeliveryMethod,options=>options.MapFrom(S=>S.DeliveryMethod.ShortName))
                .ForMember(D=>D.DeliveryMethodCost,options=>options.MapFrom(S=>S.DeliveryMethod.Cost));


            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, options => options.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.ProductName, options => options.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureUrl, options => options.MapFrom(S => $"{configuration["BaseUrl"]}{S.Product.PictureUrl}"));
        }   
    }
}
