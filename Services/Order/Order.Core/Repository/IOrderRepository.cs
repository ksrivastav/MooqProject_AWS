using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Repository
{
    public interface IOrderRepository<T>
    {
        Task<List<T>> getAllItem();
        //T getSingleItem(string a, string b);

        Task<T> getSingleItem(long id);
        Task<T> getOrderByUsername(string userName);

        Task<T> insertSingleItem(T t);
        //Task<T> updateSingleItem(T t);
        //void deleteSingleItem(long id);

        ////Task<OrderStatus> GetOrderStatus(long id);


        //Task<List<T>> getAllItemforUser(long id);
        //Task<List<T>> getAllItemforProduct(long id);
        //Task<List<T>> getAllItemforSeller(long id);
    }
}
