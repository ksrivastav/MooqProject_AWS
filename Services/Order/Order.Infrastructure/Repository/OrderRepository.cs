using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.Core.Repository;
using Order.Infrastructure.AppDbContext;
namespace Order.Infrastructure.Repository
{
    public class OrderRepository: IOrderRepository<Order.Core.Entities.Order>
    {
        DaffECommerceDbContext _dbContext;
        public OrderRepository(DaffECommerceDbContext dbContext)
        { _dbContext = dbContext; }
        public async Task<List<Order.Core.Entities.Order>> getAllItem()
        {
            List<Order.Core.Entities.Order> OrderList = new List<Order.Core.Entities.Order>();
            OrderList = await _dbContext.Order.ToListAsync<Order.Core.Entities.Order>() ;
            //.Orders.Include("Order").ToList<Order.Core.Entities.Order>();
            return OrderList;
        }
        //T getSingleItem(string a, string b);

        public async Task<Order.Core.Entities.Order> getSingleItem(long id)
        {
            Order.Core.Entities.Order Order = new Order.Core.Entities.Order();
            Order = await _dbContext.Order
            //.Include(x => x.OrderStatus)
            //.Include(y => y.Payment)
            //.Include(x => x.Payment.PaymentStatus)
            //.Include(x => x.User)
            //.Include(x => x.Product)
            .FirstAsync<Order.Core.Entities.Order>(x => x.Id == id);
            // FindAsync<Order.Core.Entities.Order>(x=> x. );
            return Order;
        }
        
        public async Task<Order.Core.Entities.Order> getOrderByUsername(string userName)
        {
            Order.Core.Entities.Order Order = new Order.Core.Entities.Order();
            Order = await  _dbContext.Order.FirstAsync(x=> x.UserName == userName); 
            return Order;
        }

        public async Task<Order.Core.Entities.Order> insertSingleItem(Order.Core.Entities.Order Order)
        {
            await _dbContext.Order.AddAsync(Order);
            var result = await _dbContext.SaveChangesAsync();
            return Order;
        }

    }
}
