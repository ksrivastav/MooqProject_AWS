using Catalog.Application.Responces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Querries
{
    public class GetAllProductCategoryQuerry:IRequest<IList<ProductCategoryResponce>>
    {


    }
}
