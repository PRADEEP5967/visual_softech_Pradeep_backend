using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication3.Data;
using WebApplication3.Models;
using static WebApplication3.DTOs.AuthDto;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IConfiguration _config;

        public AuthController(ApplicationDbContext db, IPasswordHasher<User> hasher, IConfiguration config)
        {
            _db = db;
            _hasher = hasher;
            _config = config;
        }

        // ---------------------- REGISTER -------------------------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest(new { error = "Username already exists" });

            var user = new User { Username = dto.Username };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        // ---------------------- LOGIN -------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null)
                return Unauthorized(new { error = "Invalid username or password" });

            var verify = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (verify == PasswordVerificationResult.Failed)
                return Unauthorized(new { error = "Invalid username or password" });

            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        // ---------------------- GENERATE JWT TOKEN -------------------------
        private string GenerateJwtToken(User user)
        {
            var jwtConfig = _config.GetSection("Jwt");

            string key = jwtConfig["Key"];
            string issuer = jwtConfig["Issuer"];
            string audience = jwtConfig["Audience"];

            if (string.IsNullOrEmpty(key))
                throw new Exception("JWT Key missing from appsettings.json");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("username", user.Username)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
