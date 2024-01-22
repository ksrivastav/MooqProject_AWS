using AutoMapper;
using Catalog.Application.Responces;
using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Application.Command;
namespace Catalog.Application.Mapper
{
    public class ProductMapper: Profile
    {
        public ProductMapper() { 
        
            CreateMap<Product,ProductResponce>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();

        }

    }
}
