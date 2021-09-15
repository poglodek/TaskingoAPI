using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;

namespace TaskingoAPI.Services.Seeders
{
    public class RoleSeeder
    {
        private readonly TaskingoDbContext _taskingoDbContext;

        public RoleSeeder(TaskingoDbContext taskingoDbContext)
        {
            _taskingoDbContext = taskingoDbContext;
        }
        public void Seed()
        {
            if (!_taskingoDbContext.Roles.Any())
            {
                _taskingoDbContext.Roles.AddRange(GetDefaultRoles());
                _taskingoDbContext.SaveChanges();
            }
        }

        private IEnumerable<Role> GetDefaultRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    RoleName = "Default"
                },
                new Role()
                {
                    RoleName = "Admin"
                },

            };
            return roles;
        }
    }
}

