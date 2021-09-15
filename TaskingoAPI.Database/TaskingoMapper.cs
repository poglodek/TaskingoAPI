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
            CreateMap<Database.Entity.User, UserCreatedDto>()
                .ForMember(x=> x.Role, z=>z.MapFrom(c =>c.Role.RoleName))
                .ReverseMap();
            CreateMap<Database.Entity.User, UserLoginDto>()
                .ReverseMap();
            CreateMap<Database.Entity.User, UserDto>()
                .ForMember(x => x.Role, z => z.MapFrom(c => c.Role.RoleName))
                .ReverseMap();
            CreateMap<Database.Entity.WorkTask, WorkTaskCreatedDto>()
                .ForMember(x => x.WorkGroup, z => z.MapFrom(c => c.WorkGroup.RoleName))
                .ReverseMap();
            CreateMap<Database.Entity.WorkTask, WorkTaskDto>()
                .ForMember(x => x.WorkGroup, z => z.MapFrom(c => c.WorkGroup.RoleName))
                .ForMember(x => x.WhoCreated, z => z.MapFrom(c => c.WhoCreated))
                .ForMember(x => x.AssignedUser, z => z.MapFrom(c => c.AssignedUser))
                .ReverseMap();
            CreateMap<Database.Entity.WorkTime, WorkTimeDto>().ReverseMap();
        }
    }
}
