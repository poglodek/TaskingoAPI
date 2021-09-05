using System.Security.Claims;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IUserContextServices
    {
        ClaimsPrincipal GetUser { get; }

        int GetUserId();
    }
}