using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin")]
    public class AdminReportController : ControllerBase
    {
        private readonly IAdminReportService _reportService;

        public AdminReportController(IAdminReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("best-selling-products")]
        public IActionResult BestSellingProducts()
        {
            var products = _reportService.GetBestSellingProducts();
            return Ok(products);
        }

        [HttpGet("top-rated-products")]
        public IActionResult TopRatedProducts()
        {
            var products = _reportService.GetTopRatedProducts();
            return Ok(products);
        }

        [HttpGet("revenue-by-day")]
        public IActionResult RevenueByDay(DateTime date)
        {
            var revenue = _reportService.GetRevenueReportByDay(date);
            return Ok(revenue);
        }

        [HttpGet("revenue-by-month")]
        public IActionResult RevenueByMonth(int month, int year)
        {
            var revenue = _reportService.GetRevenueReportByMonth(month, year);
            return Ok(revenue);
        }

        [HttpGet("most-active-customers")]
        public IActionResult MostActiveCustomers()
        {
            var users = _reportService.GetMostActiveCustomers();
            return Ok(users);
        }
    }

}
