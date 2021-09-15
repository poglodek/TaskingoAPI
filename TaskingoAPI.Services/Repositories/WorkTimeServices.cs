using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskingoAPI.Database;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto.WorkTime;
using TaskingoAPI.Exceptions;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class WorkTimeServices : IWorkTimeServices
    {
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IUserServices _userServices;
        private readonly IUserContextServices _userContextServices;
        private readonly IMapper _mapper;
        public WorkTimeServices(TaskingoDbContext taskingoDbContext,
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

        public List<WorkTimeDto> GetWorkTimeDtoByUserId(int userId)
        {
            var user = _userServices.GetUserById(userId);
            var workTimes = _taskingoDbContext
                .WorkTimes
                .Where(x => x.User == user)
                .ToList();
            var workTimesDto = _mapper.Map<List<WorkTimeDto>>(workTimes);
            return workTimesDto;
        }

        public void StartWorkingTime()
        {
            var user = _userServices.GetUserById(_userContextServices.GetUserId());
            if (IsWorkTimeActive(user)) throw new ConflictExceptions("WorkTime is already started.");
            var workTime = new WorkTime();
            workTime.User = user;
            workTime.WorkTimeStart = DateTime.Now;
            workTime.WorkTimeEnd = null;
            workTime.BreakTimeInMinutes = 0;
            user.ActualStatus = "Online";
            _taskingoDbContext.WorkTimes.Add(workTime);
            _taskingoDbContext.SaveChanges();
        }
        public void StopWorkingTime()
        {
            var user = _userServices.GetUserById(_userContextServices.GetUserId());
            if (!IsWorkTimeActive(user)) throw new ConflictExceptions("WorkTime is not started.");
            var workTime = GetLastActiveWorkTime(user);
            workTime.WorkTimeEnd = DateTime.Now;
            user.ActualStatus = "Offline";
            _taskingoDbContext.SaveChanges();
        }

        public void AddBreakTime(int minutes)
        {
            var user = _userServices.GetUserById(_userContextServices.GetUserId());
            if (!IsWorkTimeActive(user)) throw new ConflictExceptions("WorkTime is not started.");
            var workTime = GetLastActiveWorkTime(user);
            workTime.BreakTimeInMinutes += minutes;
            _taskingoDbContext.SaveChanges();
        }

        public int GetBreakTime()
        {
            var user = _userServices.GetUserById(_userContextServices.GetUserId());
            if (!IsWorkTimeActive(user)) throw new ConflictExceptions("WorkTime is not started.");
            var workTime = GetLastActiveWorkTime(user);
            return workTime.BreakTimeInMinutes;
        }

        private WorkTime GetLastActiveWorkTime(User user)
        {
            var workTime = _taskingoDbContext
                .WorkTimes
                .Where(x => x.User == user && x.WorkTimeEnd == null)
                .FirstOrDefault();
            return workTime;
        }

        private bool IsWorkTimeActive(User user)
        {
            var workTimes = _taskingoDbContext
                .WorkTimes
                .Count(x => x.User == user && x.WorkTimeEnd == null);
            return workTimes > 0;
        }
    }
}
