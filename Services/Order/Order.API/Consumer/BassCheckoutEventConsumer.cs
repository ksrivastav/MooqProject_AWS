using MassTransit;
using Infrastructure.EventBus.Events;
using MediatR;
using Order.Application.Command;
using Order.Application.Mapper;

namespace Order.API.Consumer
{
    public class BassCheckoutEventConsumer : IConsumer<BasketCheckoutEvent>
    {
        private IMediator mediator;
        public BassCheckoutEventConsumer(IMediator _mediator) 
        {
            mediator= _mediator;
        }


        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var msg = context.Message;
            await Console.Out.WriteLineAsync(msg.ToString());
           //var OrderCommand = LazyMapper.MapperLazy.Map< BasketCheckoutEvent,CreateOrderCommand >(context);
            CreateOrderCommand coc = new CreateOrderCommand();
            coc.UserName = msg.UserName;
            coc.CardNumber=msg.CardNumber;
            coc.CorrelationId=msg.CorrelationId;
            var i = await mediator.Send(coc);
            await Console.Out.WriteLineAsync(i.ToString());
        


        }

        
    }
}
