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
        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            using (var context = new FileSharingDbContext())
            {
                var userExisted = context.Users.Where(u => u.Username == user.Username).FirstOrDefault();
                if (userExisted != null)
                {
                    return StatusCode(409, $"User {user.Username} already exists");
                }
                user.Password = HashPassword(user.Password);
                context.Users.Add(user);
                context.SaveChanges();
                return Created($"api/users/{user.UserId}", user);
            };

            return Ok(user);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser([FromBody] User user) {
            if (UserAuthenticated(user)) {
                var token = GenerateToken(user);
                return Ok(new {token = token});
            }
            return Unauthorized(new { message = "Invalid username or password"});
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public ActionResult GetUser(int id)
        {
            using (var context = new FileSharingDbContext())
            {
                var user = context.Users.Where(u => u.UserId == id).FirstOrDefault();

            }
            return Ok();
        }

        // Check if the username exists in the database or not
        private bool UserAuthenticated(User user)
        {
            using (var context = new FileSharingDbContext())
            {
                var userExisted = context.Users.Where(u => u.Username == user.Username && Verifypassword(u.Password, user.Password)).FirstOrDefault();
                return userExisted != null;
            }

        }

        private string HashPassword(string password) {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        private bool Verifypassword(string password, string hashedPassword) {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        // returns the UserId reteived from the token
        private string GetUserId()
        {
            return HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        // Generates Token based on the userId with a 7-day expiry date
        [HttpPost]
        [Route("token")]
        public ActionResult GenerateToken([FromBody] User user)
        {
            if (!UserAuthenticated(user))
            {
                return Unauthorized("Ivalid username or password");
            }
            var expires = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                       {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username) // Use standard claim types as much as possible
                       }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                           SecurityAlgorithms.HmacSha512Signature), // Ensure this matches the algorithm used in token validation
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token)});
        }
    }
}