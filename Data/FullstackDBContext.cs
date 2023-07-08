using Fullstack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.API.Data
{
    public class FullstackDBContext:DbContext
    {
        public FullstackDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
