using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskingoAPI.Dto;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class WorkTaskServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IUserContextServices _userContextServices;
        private readonly IMapper _mapper;

        public WorkTaskServices(TaskingoDbContext taskingoDbContext,
            IUserContextServices userContextServices,
            IMapper mapper
            )
        {
            _taskingoDbContext = taskingoDbContext;
            _userContextServices = userContextServices;
            _mapper = mapper;
        }
    }
}
