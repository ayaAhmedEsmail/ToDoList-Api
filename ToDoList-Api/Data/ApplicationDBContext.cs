using Microsoft.EntityFrameworkCore;
using ToDoList_Api.Controllers;


namespace ToDoList_Api.Data
{
    public class ApplicationDBContext:DbContext
    {
        public readonly DbContextOptions _dbContext;
  
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tasks>().ToTable("Tasks").HasKey("Id");
        }
    }
}
