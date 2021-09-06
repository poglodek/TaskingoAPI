using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto;
using TaskingoAPI.Dto.WorkTask;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class WorkTaskServices : IWorkTaskServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IUserServices _userServices;
        private readonly IUserContextServices _userContextServices;
        private readonly IMapper _mapper;

        public WorkTaskServices(TaskingoDbContext taskingoDbContext,
            IUserServices userServices,
            IUserContextServices userContextServices,
            IMapper mapper
            )
        {
            _taskingoDbContext = taskingoDbContext;
            _userServices = userServices;
            _userContextServices = userContextServices;
            _mapper = mapper;
        }

        public int CreateNewTask(WorkTaskCreatedDto workTaskCreatedDto)
        {
            var workTask = _mapper.Map<WorkTask>(workTaskCreatedDto);
            workTask.CreatedTime = DateTime.Now;
            workTask.IsAssigned = false;
            workTask.WhoCreated = _userServices.GetUserById(_userContextServices.GetUserId());
            _taskingoDbContext.WorkTasks.Add(workTask);
            _taskingoDbContext.SaveChanges();
            return workTask.Id;
        }
    }
}
