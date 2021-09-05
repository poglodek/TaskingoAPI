using AutoMapper;
using TaskingoAPI.Dto.User;

namespace TaskingoAPI.Dto
{
    public class TaskingoMapper : Profile
    {
        public TaskingoMapper()
        {
            CreateMap<Entity.User, UserCreatedDto>().ReverseMap();
            CreateMap<Entity.User, UserLoginDto>().ReverseMap();
            CreateMap<Entity.User, UserDto>().ReverseMap();
        }
    }
}
