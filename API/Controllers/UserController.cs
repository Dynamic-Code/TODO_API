using API.Context;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        private ApplicationDbContext _dbContext { get; }

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Register>> Register(Register register)
        {
            var user = new User();
            user.UserId = Guid.NewGuid();
            user.UserName = register.UserName;
            user.Password = register.Password;
            user.Email = register.Email;


            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
