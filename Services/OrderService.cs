using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;

namespace E_CommerceSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IProductService _productService;
        private readonly IOrderProductsService _orderProductsService;
        private readonly IEmailService _emailService;
        private readonly IUserRepo _userRepo;

        public OrderService(
            IOrderRepo orderRepo,
            IProductService productService,
            IOrderProductsService orderProductsService,
            IEmailService emailService,
            IUserRepo userRepo)
        {
            _orderRepo = orderRepo;
            _productService = productService;
            _orderProductsService = orderProductsService;
            _emailService = emailService;
            _userRepo = userRepo;
        }
        // Update Order
        public void UpdateOrder(Order order)
        {
            _orderRepo.UpdateOrder(order);
        }


        // Place Order
        public void PlaceOrder(List<OrderItemDTO> items, int uid)
        {
            if (items == null || items.Count == 0)
                throw new Exception("Order items cannot be empty.");

            decimal totalOrderPrice = 0;
            Product product = null;

            // Validate stock
            foreach (var item in items)
            {
                product = _productService.GetProductByName(item.ProductName);
                if (product == null)
                    throw new Exception($"{item.ProductName} not found.");
                if (product.Stock < item.Quantity)
                    throw new Exception($"{item.ProductName} is out of stock.");
            }

            // Create new Order
            var order = new Order
            {
                UID = uid,
                OrderDate = DateTime.Now,
                TotalAmount = 0,
                Status = "Placed"
            };
            _orderRepo.AddOrder(order); // Save order to get OID

            // Process each item
            foreach (var item in items)
            {
                product = _productService.GetProductByName(item.ProductName);

                // Deduct stock
                product.Stock -= item.Quantity;
                _productService.UpdateProduct(product);

                // Calculate total
                totalOrderPrice += item.Quantity * product.Price;

                // Create OrderProducts
                var orderProduct = new OrderProducts
                {
                    OID = order.OID,
                    PID = product.PID,
                    Quantity = item.Quantity
                };
                _orderProductsService.AddOrderProducts(orderProduct);
            }

            // Update order total
            order.TotalAmount = totalOrderPrice;
            _orderRepo.UpdateOrder(order);

            // Send email notification
            var user = _userRepo.GetUserById(uid);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                _emailService.SendEmail(
                    user.Email,
                    "Order Placed",
                    $"Your order #{order.OID} has been placed successfully. Total: {order.TotalAmount:C}");
            }
        }


        // Get order details for a specific order and user
        public IEnumerable<OrdersOutputOTD> GetOrderById(int oid, int uid)
        {
            var items = new List<OrdersOutputOTD>();
            var order = _orderRepo.GetOrderById(oid);

            if (order == null || order.UID != uid)
                throw new InvalidOperationException("Order not found for this user.");

            var orderProducts = _orderProductsService.GetOrdersByOrderId(oid);

            foreach (var op in orderProducts)
            {
                var product = _productService.GetProductById(op.PID);
                items.Add(new OrdersOutputOTD
                {
                    ProductName = product.ProductName,
                    Quantity = op.Quantity,
                    OrderDate = order.OrderDate,
                    TotalAmount = op.Quantity * product.Price
                });
            }

            return items;
        }

        // Get all orders for a specific user
        public IEnumerable<Order> GetOrderByUserId(int uid)
        {
            var orders = _orderRepo.GetOrderByUserId(uid);
            if (orders == null || !orders.Any())
                throw new KeyNotFoundException($"No orders found for user ID {uid}.");

            return orders;
        }
        //
        public void DeleteOrder(int oid)
        {
            var order = _orderRepo.GetOrderById(oid);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {oid} not found.");

            _orderRepo.DeleteOrder(oid);
        }

        // Cancel Order
        public void CanselOrder(int oid)
        {
            var order = _orderRepo.GetOrderById(oid);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {oid} not found.");

            if (order.Status == "Cancelled")
                throw new InvalidOperationException("Order is already cancelled.");

            // Restore stock
            var orderProducts = _orderProductsService.GetOrdersByOrderId(oid);
            foreach (var op in orderProducts)
            {
                var product = _productService.GetProductById(op.PID);
                if (product != null)
                {
                    product.Stock += op.Quantity;
                    _productService.UpdateProduct(product);
                }
            }



            // Update order status
            order.Status = "Cancelled";
            _orderRepo.UpdateOrder(order);

            // Send email notification
            var user = _userRepo.GetUserById(order.UID);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                _emailService.SendEmail(
                    user.Email,
                    "Order Cancelled",
                    $"Your order #{order.OID} has been cancelled successfully.");
            }
        }

        public List<OrderProducts> GetAllOrders(int uid)
        {
            var orders = _orderRepo.GetOrderByUserId(uid);
            if (orders == null || !orders.Any())
                throw new InvalidOperationException($"No orders found for user ID {uid}.");

            var allOrderProducts = new List<OrderProducts>();

            foreach (var order in orders)
            {
                var orderProducts = _orderProductsService.GetOrdersByOrderId(order.OID);
                if (orderProducts != null)
                    allOrderProducts.AddRange(orderProducts);
            }

            return allOrderProducts;
        }
        public void AddOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orderRepo.AddOrder(order);
        }

    }
}
