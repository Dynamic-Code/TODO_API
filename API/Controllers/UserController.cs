using API.Context;
using API.Migrations;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        private ApplicationDbContext _dbContext { get; }

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDetails>> Register(Register register)
        {
            var user = new User();
            user.UserId = Guid.NewGuid().ToString();
            user.UserName = register.UserName;
            user.Email = register.Email;
            var userFromDb = await _dbContext.users.SingleOrDefaultAsync(x => x.UserName == register.UserName);
            if (userFromDb != null) return Unauthorized("Username Already Exists");
            using var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
            user.PasswordSalt = hmac.Key;
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
            //HttpContext.Session.SetString("UserId", user.UserId);
            return new UserDetails
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,

            };
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDetails>> Login(Login login)
        {
            var user = await _dbContext.users.SingleOrDefaultAsync(x => x.UserName == login.UserName);
            if (user==null) return Unauthorized("Incorrect Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for(var i =0; i<computedPass.Length; i++)
            {
                if (user.PasswordHash[i] != computedPass[i]) return Unauthorized("Incorrect Password");
            }
            //HttpContext.Session.SetString("UserId", user.UserId);

            return new UserDetails
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,

            };
        }
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout() {
            HttpContext.Session.Clear();
            return Ok(new { Logout = true });
        }
    }
}
