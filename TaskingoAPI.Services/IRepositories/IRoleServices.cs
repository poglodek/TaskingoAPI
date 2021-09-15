using System.Collections.Generic;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto.Role;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IRoleServices
    {
        int AddNewRole(string roleName);
        public Role GetDefaultRole();
        public Role GetRoleByName(string roleName);
        public List<RoleDto> GetAllRoles();
    }
}