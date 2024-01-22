using AutoMapper;
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
    public class GetProductByIdRequestHandler: IRequestHandler<GetProductByIdQuerry, ProductResponce>
    {
        IProductRepository<Product> productRepository;
        public GetProductByIdRequestHandler(IProductRepository<Product> _productRepository) {

            productRepository= _productRepository;
        }
        public async Task<ProductResponce> Handle(GetProductByIdQuerry querry, CancellationToken cancellationToken)
        {

            var ProdRes = await productRepository.getSingleItem(querry.id);
            var prodResponce =  LazyMapper.MapperLazy.Map<ProductResponce>(ProdRes);
            return prodResponce;
        }
    }
}
