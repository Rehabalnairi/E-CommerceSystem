using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Repositories
{
    public interface IOrderProductsRepo
    {
        void AddOrderProducts(OrderProducts orderProduct);
        IEnumerable<OrderProducts> GetAllOrders();
        List<OrderProducts> GetOrdersByOrderId(int oid);

        IEnumerable<OrderProducts> GetAllOrderProducts();


    }
}