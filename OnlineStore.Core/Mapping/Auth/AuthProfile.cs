using AutoMapper;
using OnlineStore.Core.DTOs.Auth;
using OnlineStore.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Mapping.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile() 
        { 
            CreateMap<Address, AddressDTO>().ReverseMap(); 
        }
    }
}
