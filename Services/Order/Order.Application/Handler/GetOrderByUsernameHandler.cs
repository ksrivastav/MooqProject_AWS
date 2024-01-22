using MediatR;
using Order.Application.Mapper;
using Order.Application.Querry;
using Order.Application.Responce;
using Order.Core.Entities;
using Order.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Handler
{
    
    public class GetOrderByUsernameHandler : IRequestHandler<GetOrderByUsernameQuerry,OrderResponce>
    {
        IOrderRepository<Order.Core.Entities.Order> orderRepository;
        public GetOrderByUsernameHandler(IOrderRepository<Order.Core.Entities.Order> _orderRepository) 
        {
            orderRepository = _orderRepository;
        }

        public async Task<OrderResponce> Handle(GetOrderByUsernameQuerry querry, CancellationToken cancellationToken)
        {
            var orderSource = await orderRepository.getOrderByUsername(querry.userName);
            var result =  LazyMapper.MapperLazy.Map<Order.Core.Entities.Order, OrderResponce>(orderSource);
            return result;
        }


    }
}
