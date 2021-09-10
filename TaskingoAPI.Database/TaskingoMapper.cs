using AutoMapper;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Dto.WorkTask;
using TaskingoAPI.Dto.WorkTime;

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
            CreateMap<Database.Entity.WorkTask, WorkTaskDto>()
                .ForMember(x => x.WhoCreated, z => z.MapFrom(c => c.WhoCreated))
                .ForMember(x => x.AssignedUser, z => z.MapFrom(c => c.AssignedUser))
                .ReverseMap();
            CreateMap<Database.Entity.WorkTime, WorkTimeDto>().ReverseMap();
        }
    }
}
