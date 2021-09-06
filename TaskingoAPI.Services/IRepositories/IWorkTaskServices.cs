using TaskingoAPI.Dto.WorkTask;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IWorkTaskServices
    {
        int CreateNewTask(WorkTaskCreatedDto workTaskCreatedDto);
        void DeleteTaskById(int id);
    }
}