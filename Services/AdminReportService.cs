using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_CommerceSystem.Services
{
    public class AdminReportService : IAdminReportService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderProductsService _orderProductsService;
        private readonly IProductService _productService;
        private readonly IUserRepo _userRepo;
        private readonly IReviewRepo _reviewRepo;

        public AdminReportService(
            IOrderRepo orderRepo,
            IOrderProductsService orderProductsService,
            IProductService productService,
            IUserRepo userRepo,
            IReviewRepo reviewRepo)
        {
            _orderRepo = orderRepo;
            _orderProductsService = orderProductsService;
            _productService = productService;
            _userRepo = userRepo;
            _reviewRepo = reviewRepo;
        }

        
        public IEnumerable<Product> GetBestSellingProducts(int top = 10)
        {
            var products = _orderProductsService.GetAllOrderProducts()
                .GroupBy(op => op.PID)
                .Select(g => new { ProductId = g.Key, QuantitySold = g.Sum(op => op.Quantity) })
                .OrderByDescending(x => x.QuantitySold)
                .Take(top)
                .ToList();

            var topProducts = products.Select(p => _productService.GetProductById(p.ProductId));
            return topProducts;
        }

      
        public IEnumerable<Product> GetTopRatedProducts(int top = 10)
        {
            return _productService.GetAllProducts(1,top)
                .OrderByDescending(p => p.OverallRating)
                .Take(top)
                .ToList();
        }

        public decimal GetRevenueReportByDay(DateTime date)
        {
            var orders = _orderRepo.GetAllOrders()
                .Where(o => o.OrderDate.Date == date.Date)
                .ToList();

            decimal totalRevenue = 0;
            foreach (var order in orders)
            {
                var orderProducts = _orderProductsService.GetOrdersByOrderId(order.OID);
                totalRevenue += orderProducts.Sum(op => op.Price * op.Quantity);
            }
            return totalRevenue;
        }

       
        public decimal GetRevenueReportByMonth(int month, int year)
        {
            var orders = _orderRepo.GetAllOrders()
                .Where(o => o.OrderDate.Month == month && o.OrderDate.Year == year)
                .ToList();

            decimal totalRevenue = 0;
            foreach (var order in orders)
            {
                var orderProducts = _orderProductsService.GetOrdersByOrderId(order.OID);
                totalRevenue += orderProducts.Sum(op => op.Price * op.Quantity);
            }
            return totalRevenue;
        }

  
        public IEnumerable<User> GetMostActiveCustomers(int top = 10)
        {
            var users = _orderRepo.GetAllOrders()
                .GroupBy(o => o.UID)
                .Select(g => new { UserId = g.Key, OrderCount = g.Count() })
                .OrderByDescending(x => x.OrderCount)
                .Take(top)
                .ToList();

            return users.Select(u => _userRepo.GetUserById(u.UserId));
        }
    }
}
