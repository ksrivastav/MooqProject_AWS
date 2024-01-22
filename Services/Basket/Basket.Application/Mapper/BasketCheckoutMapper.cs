using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Application.Responce;
using Basket.Core.Domain.Entities;
using Infrastructure.EventBus.Events;
namespace Basket.Application.Mapper
{
    public class BasketCheckoutMapper : Profile
    {
        public BasketCheckoutMapper() 
        { 
            CreateMap<BasketCheckout,BasketCheckoutResponce>().ReverseMap();
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
            
        }
    }
}
