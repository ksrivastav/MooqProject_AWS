using Basket.Core.Domain.Entities;

namespace Basket.Application.Responce
{
    public class ShoppingCartResponce
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice { get; set; }

    }
}
