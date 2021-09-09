using Microsoft.EntityFrameworkCore;
using TaskingoAPI.Database.Entity;

namespace TaskingoAPI.Database
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WorkTask>()
                .HasOne(x => x.AssignedUser)
                .WithMany(x => x.WorkTasksAssigned);
            modelBuilder.Entity<WorkTask>()
                .HasOne(x => x.WhoCreated)
                .WithMany(x => x.WorkTasks);
        }
    }
}


