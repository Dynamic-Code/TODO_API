using API.Context;
using API.Entities;
using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Controllers
{
    public class TodoController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        public TodoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("AddTodo")]
        public async Task<ActionResult<Todos>> AddTodo(Todos todos)
        {
            var todoTodb = new TodoData();
            todoTodb.Todo = todos.Todo;
            todoTodb.Status = todos.Status;
            todoTodb.UserId = HttpContext.Session.GetString("UserId");

            await _dbContext.AddAsync(todoTodb);
            _dbContext.SaveChanges();
            return new Todos
            {
                Todo = todoTodb.Todo,
                Status = todos.Status
            };
        }

        [HttpPost("GetTodo")]
        public async Task<ActionResult<List<TodoData>>> GetTodo(string UserId)
        {
            List<TodoData> todoFromDb = await _dbContext.TodoDatas.Where(x => x.UserId == UserId).ToListAsync();

            if (todoFromDb == null) return Ok("No Todo Found");


            return todoFromDb;
        }
    }
}
