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
            if (products == null || !products.Any())
                return NotFound("No products found");
            return Ok(products);
        }

        [HttpGet("top-rated-products")]
        public IActionResult TopRatedProducts()
        {
            var products = _reportService.GetTopRatedProducts();
            return Ok(products);
        }

        [HttpGet("revenue-by-day")]
        public IActionResult RevenueByDay([FromQuery] DateTime date)
        {
            if (date > DateTime.UtcNow)
                return BadRequest("Date cannot be in the future");

            var revenue = _reportService.GetRevenueReportByDay(date);
            if (revenue == null)
                return NotFound("No revenue data for the given date");

            return Ok(revenue);
        }

        [HttpGet("revenue-by-month")]
        public IActionResult RevenueByMonth([FromQuery] int month, [FromQuery] int year)
        {
            if (month < 1 || month > 12)
                return BadRequest("Month must be between 1 and 12");

            var revenue = _reportService.GetRevenueReportByMonth(month, year);

            if (revenue == null)
                return NotFound("No revenue data for the given month/year");

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
