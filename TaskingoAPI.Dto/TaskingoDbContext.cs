using Microsoft.EntityFrameworkCore;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto.Entity;

namespace TaskingoAPI.Dto
{
    public class TaskingoDbContext : DbContext
    {
        public TaskingoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }
        public DbSet<WorkTime> WorkTimes { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}


