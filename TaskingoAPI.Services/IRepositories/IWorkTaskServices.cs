using System.Collections.Generic;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto.WorkTask;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IWorkTaskServices
    {
        int CreateNewTask(WorkTaskCreatedDto workTaskCreatedDto);
        void DeleteTaskById(int id);
        List<WorkTaskDto> GetTaskByMonth(int month, int year);
        void CompleteWorkTask(WorkTaskCompletedDto workTaskCompletedDto);
        List<WorkTask> GetTaskStatus(string status);
        WorkTaskDto GetWorkTaskDtoById(int id);
        WorkTask GetTaskById(int id);
        void UpdateWorkTask(WorkTaskUpdateDto workTaskUpdateDto);
        
    }
}