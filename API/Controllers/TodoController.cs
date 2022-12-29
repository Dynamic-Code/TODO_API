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
        public async Task<ActionResult<AddTodo>> AddTodo(AddTodo todos)
        {
            var todoTodb = new TodoData();
            todoTodb.UserId = todos.userId;

            //if (todoTodb.UserId == null) return BadRequest("Please Login First");
            todoTodb.TodoHeader = todos.TodoHeader;
            todoTodb.TodoContent = todos.TodoContent;
            todoTodb.Status = false; //pending

            await _dbContext.AddAsync(todoTodb);
            _dbContext.SaveChanges();
            return new AddTodo
            {
                TodoHeader = todoTodb.TodoHeader,
                TodoContent = todoTodb.TodoContent,
                Status = todos.Status
            };
        }

        [HttpPost("GetTodo")]
        public async Task<ActionResult<List<TodoData>>> GetTodo([FromBody] string UserId)
        {
            List<TodoData> todoFromDb = await _dbContext.TodoDatas.Where(x => x.UserId == UserId).ToListAsync();

            if (todoFromDb == null) return BadRequest("No Todo Found");


            return todoFromDb;
        }
        [HttpPost("DeleteTodo")]
        public async Task<IActionResult> DeleteTodo(DeleteTodo deleteTodo)
        {
            var todoFromDb = await _dbContext.TodoDatas.Where(x => x.UserId == deleteTodo.UserId && x.Id == deleteTodo.Id).FirstOrDefaultAsync();
            if (todoFromDb == null) return BadRequest("Todo Not Found");

            _dbContext.TodoDatas.Remove(todoFromDb);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpPost("UpdateStatusTodo")]
        public async Task<IActionResult> UpdateStatusTodo(UpdateStatusTodo updateStatusTodo)
        {
            TodoData todoFromDb = await _dbContext.TodoDatas.Where(x => x.UserId == updateStatusTodo.UserId && x.Id == updateStatusTodo.Id).FirstOrDefaultAsync();
            if (todoFromDb == null) return BadRequest("Todo Not Found");

            todoFromDb.Status = !updateStatusTodo.UpdateStatus;
            _dbContext.TodoDatas.Update(todoFromDb);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
