using API.Entities;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<TodoData> TodoDatas { get; set; }
    }
}
