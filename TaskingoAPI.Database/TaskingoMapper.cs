using AutoMapper;
using TaskingoAPI.Dto.Role;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Dto.WorkTask;
using TaskingoAPI.Dto.WorkTime;

namespace TaskingoAPI.Dto
{
    public class TaskingoMapper : Profile
    {
        public TaskingoMapper()
        {
            CreateMap<UserCreatedDto, Database.Entity.User>().ForMember(x => x.Role, z=> z.Ignore());
            CreateMap<Database.Entity.User, UserLoginDto>()
                .ReverseMap();
            CreateMap<Database.Entity.User, UserDto>()
                .ForMember(x => x.Role, z => z.MapFrom(c => c.Role.RoleName))
                .ReverseMap();
            CreateMap<WorkTaskCreatedDto, Database.Entity.WorkTask>()
                .ForMember(x => x.WorkGroup, z => z.Ignore());
            CreateMap<Database.Entity.WorkTask, WorkTaskDto>()
                .ForMember(x => x.WorkGroup, z => z.MapFrom(c => c.WorkGroup.RoleName))
                .ForMember(x => x.WhoCreated, z => z.MapFrom(c => c.WhoCreated))
                .ForMember(x => x.AssignedUser, z => z.MapFrom(c => c.AssignedUser))
                .ReverseMap();
            CreateMap<Database.Entity.WorkTime, WorkTimeDto>().ReverseMap();
            CreateMap<Database.Entity.Role, RoleDto>().ReverseMap();
        }
    }
}
