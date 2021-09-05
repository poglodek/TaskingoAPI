using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskingoAPI.Exceptions;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class UserContextServices : IUserContextServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal GetUser => _httpContextAccessor.HttpContext?.User;

        public int GetUserId()
        {
            if (GetUser is null) return -1;
            try
            {
                return int.Parse(GetUser.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch
            {
                throw new NotFound("User not found.");
            }
        }
    }

}