﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Repository;
using Order.Application.Command;
using Order.Application.Querry;
namespace Order.API.Controllers
{
    [ApiController]
    //[Route("api/v{version:aspVersion}/[controller]")]
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        IMediator mediator;
        IOrderRepository<Order.Core.Entities.Order> orderRepository;

        public OrderController(IMediator _mediator, IOrderRepository<Order.Core.Entities.Order> _orderRepository)
        {
            mediator = _mediator;
            orderRepository = _orderRepository;

        }
        [HttpGet()]
        //public async Task<IActionResult> getAllProducts([FromQuery] ProductSpecs productSpecs)
        //{

        //    var querry = new GetAllProductQuerry();
        //    querry.productSpecs = productSpecs; ;
        //    var i = await mediator.Send(querry);
        //    int totalCount = await productRepository.getTotalCount(productSpecs);
        //    var result = new PaginatedList<ProductResponce>(i, totalCount, productSpecs.pageIndex, productSpecs.pageSize);
        //    // var ii = await productRepository.getAllItem();
        //    // var prodResponce = LazyMapper.MapperLazy.Map<IList<Product>, IList<ProductResponce>>(ii);
        //    return Ok(result);

        //}
        [HttpGet()]
        public async Task<IActionResult> getOrderbyUsername(string userName)
        {
            var querry = new GetOrderByUsernameQuerry(userName);
            var result = await mediator.Send(querry);
            return Ok(result);
        }
       
        [HttpPost()]
        public async Task<IActionResult> createOrder([FromBody] CreateOrderCommand OrderCommand)
        {

            var i = await mediator.Send(OrderCommand);
            //var prodResponce = LazyMapper.MapperLazy.Map<IList<ProductCategory>, IList<ProductResponce>>(ii);
            return Ok(i);

        }


    }
}