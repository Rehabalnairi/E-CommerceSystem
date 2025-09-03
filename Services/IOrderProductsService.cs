using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IOrderProductsService
    {
        void AddOrderProducts(OrderProducts product);
        IEnumerable<OrderProducts> GetAllOrders();
        IEnumerable<OrderProducts> GetProductsByOrderId(int oid);
        List<OrderProducts> GetOrdersByOrderId(int oid);
    }
}