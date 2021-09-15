using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto.WorkTask;
using TaskingoAPI.Exceptions;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class WorkTaskServices : IWorkTaskServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        private readonly IUserContextServices _userContextServices;
        private readonly IMapper _mapper;

        public WorkTaskServices(TaskingoDbContext taskingoDbContext,
            IUserServices userServices,
            IRoleServices roleServices,
            IUserContextServices userContextServices,
            IMapper mapper
            )
        {
            _taskingoDbContext = taskingoDbContext;
            _userServices = userServices;
            _roleServices = roleServices;
            _userContextServices = userContextServices;
            _mapper = mapper;
        }

        public int CreateNewTask(WorkTaskCreatedDto workTaskCreatedDto)
        {
            var workTask = _mapper.Map<WorkTask>(workTaskCreatedDto);
            workTask.CreatedTime = DateTime.Now;
            workTask.IsAssigned = false;
            workTask.WorkGroup = _roleServices.GetRoleByName(workTaskCreatedDto.WorkGroup);
            workTask.WhoCreated = _userServices.GetUserById(_userContextServices.GetUserId());
            _taskingoDbContext.WorkTasks.Add(workTask);
            _taskingoDbContext.SaveChanges();
            return workTask.Id;
        }

        public void DeleteTaskById(int id)
        {
            var task = GetTaskById(id);
            _taskingoDbContext.WorkTasks.Remove(task);
            _taskingoDbContext.SaveChanges();
        }

        public List<WorkTaskDto> GetTaskByMonth(int month, int year)
        {
            var tasks = _taskingoDbContext
                .WorkTasks
                .Include(x => x.WhoCreated)
                .Include(x =>x.WorkGroup)
                .Where(x => x.DeadLine.Month.Equals(month) && x.DeadLine.Year.Equals(year))
                .ToList();

            var tasksDto = _mapper.Map<List<WorkTaskDto>>(tasks);
            return tasksDto;

        }

        public void CompleteWorkTask(WorkTaskCompletedDto workTaskCompletedDto)
        {
            var user = _userServices.GetUserById(_userContextServices.GetUserId());
            user.ActualStatus = "Online";
            var task = GetTaskById(workTaskCompletedDto.Id);
            task.Status = "Completed";
            task.Comment = workTaskCompletedDto.Comment;
            _taskingoDbContext.SaveChanges();
        }

        public WorkTaskDto GetWorkTaskDto(int id)
        {
            var task = GetTaskById(id);
            var taskDto = _mapper.Map<WorkTaskDto>(task);
            return taskDto;
        }

        public void UpdateWorkTask(WorkTaskUpdateDto workTaskUpdateDto)
        {
            var task = GetTaskById(workTaskUpdateDto.Id);
            task.Priority = workTaskUpdateDto.Priority;
            task.Status = workTaskUpdateDto.Status;
            task.Title = workTaskUpdateDto.Title;
            task.Comment = workTaskUpdateDto.Comment;
            task.Description = workTaskUpdateDto.Description;
            task.DeadLine = workTaskUpdateDto.DeadLine;
            _taskingoDbContext.SaveChanges();
        }

        private WorkTask GetTaskById(int id)
        {
            var task = _taskingoDbContext
                .WorkTasks
                .Include(x => x.AssignedUser)
                .Include(x => x.WorkGroup)
                .Include(x => x.WhoCreated)
                .FirstOrDefault(x => x.Id == id);
            if (task is null) throw new NotFound("Task not found.");
            return task;
        }
    }
}
