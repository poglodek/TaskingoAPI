using TaskingoAPI.Database.Entity;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IRoleServices
    {
        int AddNewRole(string roleName);
        public Role GetDefaultRole();
        public Role GetRoleByName(string roleName);
    }
}