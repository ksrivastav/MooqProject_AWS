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
    public class GetAllProductCategoryRequestHandler: IRequestHandler<GetAllProductCategoryQuerry, IList<ProductCategoryResponce>>
    {
        IProductCategoryRepository<ProductCategory> ProductCategoryRepository;
        public GetAllProductCategoryRequestHandler(IProductCategoryRepository<ProductCategory> _ProductCategoryRepository) {

            ProductCategoryRepository= _ProductCategoryRepository;
        }
        public async Task<IList<ProductCategoryResponce>> Handle(GetAllProductCategoryQuerry querry, CancellationToken cancellationToken)
        {

            var ProdRes = await ProductCategoryRepository.getAllItem();
            var prodResponce =  LazyMapper.MapperLazy.Map< IList <ProductCategory> ,IList <ProductCategoryResponce>>(ProdRes);
            return prodResponce;
        }
    }
}
