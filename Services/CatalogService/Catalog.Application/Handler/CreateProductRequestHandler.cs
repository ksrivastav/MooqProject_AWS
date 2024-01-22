using AutoMapper;
using Catalog.Application.Command;
using Catalog.Application.Mapper;
using Catalog.Application.Querries;
using Catalog.Application.Responces;
using Catalog.Core.Entities;
using Catalog.Core.RepositoryContracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handler
{
    public class CreateProductRequestHandler: IRequestHandler<CreateProductCommand, ProductResponce>
    {
        IProductRepository<Product> productRepository;
        public CreateProductRequestHandler(IProductRepository<Product> _productRepository) {

            productRepository= _productRepository;
        }
        public async Task<ProductResponce> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var prodEnt = LazyMapper.MapperLazy.Map<Product>(command);
            var ProdRes = await productRepository.insertSingleItem(prodEnt);
            var prodResponce = await productRepository.getSingleItem(ProdRes);
            var resultProd = LazyMapper.MapperLazy.Map<Product, ProductResponce>(prodResponce);
            return resultProd;
        }
    }
}
