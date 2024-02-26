using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestApiTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Login with username and password and return jwt token
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // TODO: fake login email and password is admin@gmail.com and P@ssw0rd
            if (request.Email == "admin@gmail.com" && request.Password == "P@ssw0rd")
            {
                var tokenString = GenerateJwtToken(request);
                return Ok(new { token = tokenString });
            }
            return BadRequest(new { message = "Email or password is incorrect" });
        }

        private string GenerateJwtToken(LoginRequest userInfo)
        {
            Console.WriteLine(_configuration["Jwt:Key"]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Email,userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Issuer"],
                            claims,
                            null,
                            expires: DateTime.Now.AddMinutes(120),
                            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
