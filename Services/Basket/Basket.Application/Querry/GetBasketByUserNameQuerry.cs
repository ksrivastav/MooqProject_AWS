using Basket.Application.Responce;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Querry
{
    public class GetBasketByUserNameQuerry: IRequest<ShoppingCartResponce>
    {
        public string UserName { get; set; }
        public GetBasketByUserNameQuerry(string userName)
        {
            UserName = userName;
        }
    }
}
