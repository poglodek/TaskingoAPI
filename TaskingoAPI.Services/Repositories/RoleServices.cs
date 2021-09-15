using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Exceptions;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class RoleServices : IRoleServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;

        public RoleServices(TaskingoDbContext taskingoDbContext)
        {
            _taskingoDbContext = taskingoDbContext;
        }

        public int AddNewRole(string roleName)
        {
            var ifRoleExist = _taskingoDbContext
                .Roles
                .Any(x => x.RoleName.ToUpper().Equals(roleName.ToUpper()));
            if (ifRoleExist) throw new ConflictExceptions("Role exist.");
            var newRole = new Role {RoleName = roleName};
            _taskingoDbContext
                .Roles
                .Add(newRole);
            _taskingoDbContext.SaveChanges();
            return newRole.Id;
        }

    }
}
