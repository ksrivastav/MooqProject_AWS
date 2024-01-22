using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MediatR;
using Catalog.Core.Entities;
using Catalog.Application.Responces;
using Catalog.Application.Querries;
using Catalog.Core.RepositoryContracts;
using Catalog.Application.Mapper;
using System;
using Catalog.Application.Command;
using Catalog.Core.Specs;

namespace Catalog.API.Controllers
{
    [ApiController]
    //[Route("api/v{version:aspVersion}/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class CommandController : Controller
    {
        IMediator mediator;
        IProductRepository<Product> productRepository;

        public CommandController(IMediator _mediator, IProductRepository<Product> _productRepository)
        {
            mediator = _mediator;
            productRepository = _productRepository;

        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpGet("{orderByColumn?},{sortOrder?},{searchColumn?},{searchValue?}")]
        //public async Task<IActionResult> getAllProducts(string orderByColumn , string sortOrder, string searchColumn, string searchValue)
       
        [HttpGet()]
        public async Task<IActionResult> getAllProducts([FromQuery] ProductSpecs productSpecs)
        {
            
            var querry = new GetAllProductQuerry();
            querry.productSpecs = productSpecs; ;
            var i = await mediator.Send(querry);
            int totalCount = await productRepository.getTotalCount( productSpecs);
            var result = new PaginatedList<ProductResponce>(i, totalCount, productSpecs.pageIndex, productSpecs.pageSize );
           // var ii = await productRepository.getAllItem();
           // var prodResponce = LazyMapper.MapperLazy.Map<IList<Product>, IList<ProductResponce>>(ii);
            return Ok(result);
           
        }

        [HttpGet()]
        public async Task<IActionResult> getAllProductCategory()
        {
            var querry = new GetAllProductCategoryQuerry();
            var i = await mediator.Send(querry);
            //var prodResponce = LazyMapper.MapperLazy.Map<IList<ProductCategory>, IList<ProductResponce>>(ii);
            return Ok(i);

        }

        [HttpGet()]
        public async Task<IActionResult> getAllProductSubCategory()
        {
            var querry = new GetAllProductSubCategoryQuerry();
            var i = await mediator.Send(querry);
            //var prodResponce = LazyMapper.MapperLazy.Map<IList<ProductCategory>, IList<ProductResponce>>(ii);
            return Ok(i);

        }

        [HttpPost()]
        public async Task<IActionResult> createProduct([FromBody] CreateProductCommand productCommand)
        {
            
            var i = await mediator.Send(productCommand);
            //var prodResponce = LazyMapper.MapperLazy.Map<IList<ProductCategory>, IList<ProductResponce>>(ii);
            return Ok(i);

        }

    }
}
