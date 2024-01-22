
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Mapper
{
    public static class LazyMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                cfg.AddProfile<OrderMapperProfile>();
          


            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper MapperLazy { get { return lazy.Value; } }
    }
}
