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

        // Endpoint to place a new order
        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder([FromBody] List<OrderItemDTO> items)
        {
            try
            {
                // Validate input
                if (items == null || !items.Any())
                    return BadRequest("Order items cannot be empty.");

                // Extract user ID from JWT token
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userId = GetUserIdFromToken(token);
                int uid = int.Parse(userId);

                // Call service to place the order
                _orderService.PlaceOrder(items, uid);

                return Ok("Order placed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while placing the order: {ex.Message}");
            }
        }

        // Endpoint to cancel an order and restore product stock
        [HttpPost("CancelOrder/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            try
            {
                // Call service to cancel the order
                _orderService.CanselOrder(orderId);

                return Ok($"Order {orderId} has been cancelled and stock restored.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while cancelling order: {ex.Message}");
            }
        }

        // Endpoint to get all orders for the logged-in user
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                // Extract user ID from JWT token
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userId = GetUserIdFromToken(token);
                int uid = int.Parse(userId);

                // Call service to retrieve all orders
                return Ok(_orderService.GetAllOrders(uid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving orders: {ex.Message}");
            }
        }

        // Endpoint to get details of a single order for the logged-in user
        [HttpGet("GetOrderById/{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            try
            {
                // Extract user ID from JWT token
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userId = GetUserIdFromToken(token);
                int uid = int.Parse(userId);

                // Call service to retrieve order details
                return Ok(_orderService.GetOrderById(orderId, uid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the order: {ex.Message}");
            }
        }

        // Helper method to extract user ID from JWT token
        private string? GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);

                // Extract the 'sub' claim, which usually contains the user ID
                var subClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");

                return subClaim?.Value;
            }

            throw new UnauthorizedAccessException("Invalid or unreadable token.");
        }
    }
}
