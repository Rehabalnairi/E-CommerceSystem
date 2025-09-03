using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IOrderService
    {
        void PlaceOrder(List<OrderItemDTO> items, int uid);       // Place a new order
        void CanselOrder(int oid);                                // Cancel an existing order
        IEnumerable<Order> GetOrderByUserId(int uid);            // Get all orders for a user
        IEnumerable<OrdersOutputOTD> GetOrderById(int oid, int uid);  // Get order details for a user
        List<OrderProducts> GetAllOrders(int uid);               // Get all order products for a user
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void PlaceOrder(List<OrderItemDTO> items, int uid);
        void UpdateOrderStatus(int orderId, OrderStatus status);
    }
}
