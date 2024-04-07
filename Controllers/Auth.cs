using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
// using FileSharing.Api.Models.User;
using FileSharing.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace FileSharing.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration configuration;
        private readonly FileSharingDbContext context;
        public AuthController(FileSharingDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {

            var userExisted = context.Users.Where(u => u.Username == user.Username).FirstOrDefault();
            if (userExisted != null)
            {
                return StatusCode(409, new { message = $"User {user.Username} already exists" });
            }
            user.Password = HashPassword(user.Password);
            context.Users.Add(user);
            context.SaveChanges();
            var token = GenerateToken(user);
            return Created($"api/users/{user.UserId}", new { token = token });
            return Ok(user);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser([FromBody] User user)
        {
            if (UserAuthenticated(user))
            {
                var token = GenerateToken(user);
                return Ok(new { token = token });
            }
            return Unauthorized(new { message = "Invalid username or password" });
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public ActionResult GetUser(int id)
        {

            var userIdFromToken = GetUserId();
            if (Int32.Parse(userIdFromToken) != id)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            var user = context.Users.Where(u => u.UserId == id).FirstOrDefault();
            return Ok();
        }

        // Check if the username exists in the database or not
        private bool UserAuthenticated(User user)
        {
            var userExisted = context.Users.Where(u => u.Username == user.Username && Verifypassword(u.Password, user.Password)).FirstOrDefault();
            return userExisted != null;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        private bool Verifypassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        // returns the UserId reteived from the token
        private string GetUserId()
        {
            return HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        // Generates Token based on the userId with a 7-day expiry date
        private string GenerateToken([FromBody] User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                       {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
                       }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                           SecurityAlgorithms.HmacSha512Signature),
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            // return Ok(new { token = tokenHandler.WriteToken(token)});
        }
    }
}