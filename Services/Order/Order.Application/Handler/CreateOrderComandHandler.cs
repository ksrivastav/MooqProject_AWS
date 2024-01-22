using MediatR;
using Order.Application.Command;
using Order.Application.Mapper;
using Order.Application.Responce;
using Order.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Handler
{
    public class CreateOrderComandHandler : IRequestHandler<CreateOrderCommand, OrderResponce>
    {
        IOrderRepository<Order.Core.Entities.Order> orderRespository;
        public CreateOrderComandHandler(IOrderRepository<Order.Core.Entities.Order> _orderRespository) 
        {
            orderRespository = _orderRespository;
        }

        public async Task<OrderResponce> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderEnt = LazyMapper.MapperLazy.Map<Order.Core.Entities.Order>(command);
            var result = await orderRespository.insertSingleItem(orderEnt);
            var rest = LazyMapper.MapperLazy.Map<OrderResponce>(orderEnt);
            return rest;
        }
    }
}
