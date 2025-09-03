using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        public ApplicationDbContext _context;

        public OrderRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                return _context.Orders
                    .Include(o => o.user)
                    .Include(o => o.OrderProducts)
                        .ThenInclude(op => op.product)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public Order GetOrderById(int oid)
        {
            try
            {
                return _context.Orders
                .Include(o => o.user)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.product)
                .FirstOrDefault(o => o.OID == oid);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public IEnumerable<Order> GetOrderByUserId(int uid)
        {
            try
            {
                return _context.Orders
                .Include(o => o.user)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.product)
                .Where(o => o.UID == uid)
                .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public void AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public void DeleteOrder(int oid)
        {
            try
            {
                var order = GetOrderById(oid);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

    }
}
