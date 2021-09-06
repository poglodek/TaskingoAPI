using AutoMapper;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Dto.WorkTask;

namespace TaskingoAPI.Dto
{
    public class TaskingoMapper : Profile
    {
        public TaskingoMapper()
        {
            CreateMap<Database.Entity.User, UserCreatedDto>().ReverseMap();
            CreateMap<Database.Entity.User, UserLoginDto>().ReverseMap();
            CreateMap<Database.Entity.User, UserDto>().ReverseMap();
            CreateMap<Database.Entity.WorkTask, WorkTaskCreatedDto>().ReverseMap();
        }
    }
}
