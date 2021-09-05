using TaskingoAPI.Dto.User;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IUserServices
    {
        int RegisterUser(UserCreatedDto userDto);
        string LoginUser(UserLoginDto userLoginDto);
    }
}