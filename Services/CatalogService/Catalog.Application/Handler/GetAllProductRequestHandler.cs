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
    public class GetAllProductRequestHandler: IRequestHandler<GetAllProductQuerry, IList<ProductResponce>>
    {
        IProductRepository<Product> productRepository;
        public GetAllProductRequestHandler(IProductRepository<Product> _productRepository) {

            productRepository= _productRepository;
        }
        public async Task<IList<ProductResponce>> Handle(GetAllProductQuerry querry, CancellationToken cancellationToken)
        {
            
            var ProdRes = await productRepository.getAllItem(querry.productSpecs);
            var prodResponce = LazyMapper.MapperLazy.Map<IList<Product>, IList<ProductResponce>>(ProdRes);
            return prodResponce;
        }
    }
}
