using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace E_CommerceSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Place a new order
        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder([FromBody] List<OrderItemDTO> items)
        {
            try
            {
                if (items == null || !items.Any())
                    return BadRequest("Order items cannot be empty.");

                // Get user ID from token
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                int uid = int.Parse(GetUserIdFromToken(token));

                _orderService.PlaceOrder(items, uid);
                return Ok("Order placed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error placing order: {ex.Message}");
            }
        }

        // Cancel an order
        [HttpPost("CancelOrder/{oid}")]
        public IActionResult CancelOrder(int oid)
        {
            try
            {
                _orderService.CanselOrder(oid);
                return Ok($"Order #{oid} has been cancelled successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error cancelling order: {ex.Message}");
            }
        }

        // Get all orders for the logged-in user
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                int uid = int.Parse(GetUserIdFromToken(token));

                return Ok(_orderService.GetAllOrders(uid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving orders: {ex.Message}");
            }
        }

        // Get specific order details by order ID
        [HttpGet("GetOrderById/{oid}")]
        public IActionResult GetOrderById(int oid)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                int uid = int.Parse(GetUserIdFromToken(token));

                return Ok(_orderService.GetOrderById(oid, uid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving order: {ex.Message}");
            }
        }

        // Decode JWT to get user ID
        private string? GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                var subClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");
                return subClaim?.Value;
            }
            throw new UnauthorizedAccessException("Invalid or unreadable token.");
        }
    }
}
