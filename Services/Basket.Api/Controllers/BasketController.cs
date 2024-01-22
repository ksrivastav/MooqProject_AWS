using Basket.Application.Command;
using Basket.Application.Mapper;
using Basket.Application.Querry;
using Basket.Core.Domain.Entities;
using Basket.Core.RepositoryContracts;
using Infrastructure.EventBus.Events;
using MassTransit;
//using MassTransit.Mediator;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class BasketController : Controller
    {
        IBasketRepository basketRepository;
        IMediator mediator;
        //IPublishEndpoint publishEndpoint;
        public BasketController(IBasketRepository _basketRepository, IMediator _mediator)
        {
            mediator = _mediator;
            basketRepository = _basketRepository;
           // publishEndpoint = _publishEndpoint;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var querry = new GetBasketByUserNameQuerry(userName);
            var result = await mediator.Send(querry);
            return Ok(result);
            //var basket = await basketRepository.GetBasket(userName);
            //return Ok(basket ?? new ShoppingCart(userName));
        }

        // UpdateBasketCommand

        [HttpPost()]
        public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketCommand updateBasketCommand)
        {
            var i = await mediator.Send(updateBasketCommand);
            return Ok(i);
        }


       
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var query = new GetBasketByUserNameQuerry(basketCheckout.UserName);
            var basket = await mediator.Send(query);
            if (basket == null)
            {
                return BadRequest();
            }
            var eventMesg = LazyMapper.MapperLazy.Map<BasketCheckoutEvent>(basketCheckout);
            eventMesg.TotalPrice = basket.TotalPrice;
            eventMesg.CorrelationId = Guid.NewGuid().ToString();
            //await publishEndpoint.Publish<BasketCheckoutEvent>(eventMesg);

            //var deleteQuery = basketRepository.DeleteBasket(basketCheckout.UserName);
            return Accepted();


        }


    }
}
