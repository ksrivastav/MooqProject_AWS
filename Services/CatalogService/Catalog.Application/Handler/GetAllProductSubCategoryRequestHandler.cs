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
    public class GetAllProductSubCategoryRequestHandler: IRequestHandler<GetAllProductSubCategoryQuerry, IList<ProductSubCategoryResponce>>
    {
        IProductSubCategoryRepository<ProductSubCategory> ProductSubCategoryRepository;
        public GetAllProductSubCategoryRequestHandler(IProductSubCategoryRepository<ProductSubCategory> _ProductSubCategoryRepository) {

            ProductSubCategoryRepository= _ProductSubCategoryRepository;
        }
        public async Task<IList<ProductSubCategoryResponce>> Handle(GetAllProductSubCategoryQuerry querry, CancellationToken cancellationToken)
        {

            var ProdRes = await ProductSubCategoryRepository.getAllItem();
            var prodResponce =  LazyMapper.MapperLazy.Map< IList<ProductSubCategory> ,IList <ProductSubCategoryResponce>>(ProdRes);
            return prodResponce;
        }
    }
}
