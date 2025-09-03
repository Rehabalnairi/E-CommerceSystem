using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;


        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _authService = _authService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserDTO InputUser)
        {
            try
            {
                if(InputUser == null)
                    return BadRequest("User data is required");

                var user = new User
                {
                    UName = InputUser.UName,
                    Email = InputUser.Email,
                    Password = InputUser.Password,
                    Role = InputUser.Role,
                    Phone = InputUser.Phone,
                    CreatedAt = DateTime.Now
                };

                _userService.AddUser(user);

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Return a generic error response
                return StatusCode(500, $"An error occurred while adding the user. {ex.Message} ");
            }
        }


        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var user = _userService.GetUSer(email, password);
                string token = GenerateJwtToken(user.UID.ToString(), user.UName, user.Role);
                return Ok(token);

            }
            catch (Exception ex)
            {
                // Return a generic error response
                return StatusCode(500, $"An error occurred while login. {(ex.Message)}");
            }

        }


        [HttpGet("GetUserById/{UserID}")]
        public IActionResult GetUserById(int UserID)
        {
            try
            {
                var user = _userService.GetUserById(UserID);
                return Ok(user);
   
            }
            catch (Exception ex)
            {
                // Return a generic error response
                return StatusCode(500, $"An error occurred while retrieving user. {(ex.Message)}");
            }
        }

        [NonAction]
        public string GenerateJwtToken(string userId, string username, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Name, username),
                new Claim(JwtRegisteredClaimNames.UniqueName, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //for refresh token

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            var user = _authService.ValidateRefreshToken(refreshToken); // 
            if (user == null)
                return Unauthorized("Invalid refresh token");

            var newJwt = _authService.GenerateJwtToken(user);          
            var newRefreshToken = _authService.GenerateRefreshToken(user); 

            return Ok(new
            {
                Token = newJwt,
                RefreshToken = newRefreshToken.Token
            });
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult LoginWithCookies(string email, string password)
        {
            var user = _userService.GetUSer(email, password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var jwtToken = GenerateJwtToken(user.UID.ToString(), user.UName, user.Role);
            var refreshToken = _userService.GenerateRefreshToken(user);

          
            Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
            {
                HttpOnly = true,       
                Secure = true,       
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

         
            Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = refreshToken.Expires
            });

            return Ok(new { Message = "Logged in successfully" });
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                return Unauthorized("Refresh token missing");

            var user = _userService.ValidateRefreshToken(refreshToken);
            if (user == null)
                return Unauthorized("Invalid refresh token");

            var newJwt = GenerateJwtToken(user.UID.ToString(), user.UName, user.Role);
            var newRefreshToken = _userService.GenerateRefreshToken(user);

            Response.Cookies.Append("jwtToken", newJwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = newRefreshToken.Expires
            });

            return Ok(new { Message = "Token refreshed successfully" });
        }


    }
}
