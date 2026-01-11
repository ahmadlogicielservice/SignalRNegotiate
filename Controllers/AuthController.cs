using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalRNegotiate.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromQuery] string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name query parameter is required.");
            }

            // Validate user credentials here

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.NameIdentifier, name)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("THIS_IS_A_32+_BYTE_SUPER_SECRET_KEY_123"));

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [Authorize]
        [HttpPost("chat-token")]
        public IActionResult ChatToken()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim("hub", "chat")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("CHAT_SECRET_KEY_32_BYTES_LONG___"));

            var token = new JwtSecurityToken(
                issuer: "chat-issuer",
                audience: "chat-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(45),
                signingCredentials: new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }

}
