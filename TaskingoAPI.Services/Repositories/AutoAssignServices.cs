using System;
using System.Linq;
using TaskingoAPI.Database;
using TaskingoAPI.Services.IRepositories;

namespace TaskingoAPI.Services.Repositories
{
    public class AutoAssignServices : IAutoAssignServices
    {
        private readonly IWorkTaskServices _workTaskServices;
        private readonly TaskingoDbContext _taskingoDbContext;
        private readonly IUserServices _userServices;

        public AutoAssignServices(IWorkTaskServices workTaskServices,
            TaskingoDbContext taskingoDbContext,
            IUserServices userServices)
        {
            _workTaskServices = workTaskServices;
            _taskingoDbContext = taskingoDbContext;
            _userServices = userServices;
        }


        public void AutoAssign()
        {
            var noAssignWorkTasks = _workTaskServices
                .GetTaskStatus("In queue")
                .Where(x => !x.IsAssigned)
                .OrderBy(x=> x.DeadLine)
                .ToList();

            var freeUsersWithNoTask = _userServices
                .GetAllUser()
                .Where(x => x.WorkTasksAssigned.Count(c => c.IsAssigned) < 3)
                .ToList();

            foreach (var noAssignTask in noAssignWorkTasks)
            {
                if (noAssignTask.IsAssigned)
                    break;
                foreach (var user in freeUsersWithNoTask)
                {
                    if (noAssignTask.WorkGroup.RoleName.Equals(user.Role.RoleName))
                    {
                        if (user.WorkTasksAssigned.Count(x => x.IsAssigned) > 3)
                            break;
                        var task = _workTaskServices.GetTaskById(noAssignTask.Id);
                        if (task.IsAssigned)
                            break;
                        task.IsAssigned = true;
                        task.AssignedUser = user;
                        task.Status = "In progress";
                        _taskingoDbContext.SaveChanges();

                    }
                }
            }
        }
    }
}
