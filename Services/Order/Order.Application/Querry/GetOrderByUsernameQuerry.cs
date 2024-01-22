using MediatR;
using Order.Application.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Querry
{
    public class GetOrderByUsernameQuerry: IRequest<OrderResponce>
    {
        public string userName { get; set; }
        public GetOrderByUsernameQuerry(string _userName )
        {
            userName = _userName;   

        }


    }
}
