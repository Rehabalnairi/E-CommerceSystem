using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace E_CommerceSystem.Services
{
    public class OrderProductsService : IOrderProductsService
    {
        private readonly IOrderProductsRepo _orderProductsRepo;

        public OrderProductsService(IOrderProductsRepo orderProductsRepo)
        {
            _orderProductsRepo = orderProductsRepo;
        }

        // Add a new OrderProducts record
        public void AddOrderProducts(OrderProducts product)
        {
            _orderProductsRepo.AddOrderProducts(product);
        }

        // Get all OrderProducts records
        public IEnumerable<OrderProducts> GetAllOrders()
        {
            return _orderProductsRepo.GetAllOrders();
        }

        // Get list of OrderProducts by Order ID (List)
        public List<OrderProducts> GetOrdersByOrderId(int oid)
        {
            return _orderProductsRepo.GetOrdersByOrderId(oid);
        }

        // Get IEnumerable<OrderProducts> by Order ID (for interface)
        public IEnumerable<OrderProducts> GetProductsByOrderId(int oid)
        {
       
            return _orderProductsRepo.GetOrdersByOrderId(oid);
        }
        public IEnumerable<OrderProducts> GetAllOrderProducts()
        {
            return _orderProductsRepo.GetAllOrderProducts();
        }

    }
}
