using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IAdminReportService
    {
        IEnumerable<Product> GetBestSellingProducts(int top = 10);
        IEnumerable<Product> GetTopRatedProducts(int top = 10);
        decimal GetRevenueReportByDay(DateTime date);
        decimal GetRevenueReportByMonth(int month, int year);
        IEnumerable<User> GetMostActiveCustomers(int top = 10);
    }
}
