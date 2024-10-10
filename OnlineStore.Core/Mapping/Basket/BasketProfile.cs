using AutoMapper;
using OnlineStore.Core.DTOs.Basket;
using OnlineStore.Core.Entities.Basket_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Mapping.Basket
{
    public class BasketProfile : Profile
    {
        public BasketProfile() 
        {
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
        }
    }
}
