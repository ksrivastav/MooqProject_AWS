using AutoMapper;
using Basket.Application.Command;
using Basket.Application.Responce;
using Basket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Mapper
{
    public class ShoppingCartMapper :Profile
    {
        public ShoppingCartMapper()
        {
            CreateMap<ShoppingCart,ShoppingCartResponce>().ReverseMap();
            CreateMap<ShoppingCart, UpdateBasketCommand>().ReverseMap();

        }
    }
}
