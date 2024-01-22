using Catalog.Application.Responces;
using Catalog.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Core.Specs;

namespace Catalog.Application.Querries
{
    public class GetAllProductQuerry:IRequest<IList<ProductResponce>>
    {
       public ProductSpecs productSpecs { get; set; }

    }
}
