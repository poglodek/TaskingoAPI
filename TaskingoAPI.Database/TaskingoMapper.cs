using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
