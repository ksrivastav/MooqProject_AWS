using Catalog.Application.Responces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Querries
{
    public class GetProductByIdQuerry : IRequest<ProductResponce>
    {
        public long id;

        public GetProductByIdQuerry(long Id)
        {
            id = Id;

        }

    }
}
