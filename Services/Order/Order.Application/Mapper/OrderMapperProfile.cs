using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Order.Application.Responce;
using Order.Core.Entities;
using Order.Application.Command;
using Infrastructure.EventBus.Events;
namespace Order.Application.Mapper
{
    public class OrderMapperProfile: Profile
    {
        public OrderMapperProfile() 
        { 
            CreateMap<Order.Core.Entities.Order,OrderResponce>().ReverseMap();
            CreateMap<Order.Core.Entities.Order, CreateOrderCommand>().ReverseMap();
            CreateMap<BasketCheckoutEvent, CreateOrderCommand>().ReverseMap();
        }
    }
}
