using AutoMapper;
using Catalog.Application.Responces;
using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Mapper
{
    public class ProductSubCategoryMapper: Profile
    {
        public ProductSubCategoryMapper() { 
        
            CreateMap<ProductSubCategory,ProductSubCategoryResponce>().ReverseMap();
        
        }

    }
}
