using AutoMapper;
using Basket.Application.Responce;
using Basket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Mapper
{
    public class ShoppingCartItemMapper : Profile
    {
        public ShoppingCartItemMapper() 
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemResponce>().ReverseMap();
        }
    }
}
