using Basket.Application.Responce;
using Basket.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Command
{
    public class UpdateBasketCommand : IRequest<ShoppingCartResponce>
    {

        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice { get; set; }

    }
}
