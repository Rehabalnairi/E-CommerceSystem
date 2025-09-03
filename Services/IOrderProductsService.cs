using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IOrderProductsService
    {
        void AddOrderProducts(OrderProducts product);
        IEnumerable<OrderProducts> GetAllOrders();
        IEnumerable<OrderProducts> GetProductsByOrderId(int oid);
        List<OrderProducts> GetOrdersByOrderId(int oid);
        IEnumerable<OrderProducts> GetAllOrderProducts();
       // IEnumerable<Product> GetAllProducts(int pageNumber, int pageSize, string? name = null, decimal? minPrice = null, decimal? maxPrice = null);

        // IEnumerable<OrderProducts> GetOrdersByOrderId(int orderId);

    }
}