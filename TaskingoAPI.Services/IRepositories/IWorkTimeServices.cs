using System.Collections.Generic;
using TaskingoAPI.Dto.WorkTime;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IWorkTimeServices
    {
        List<WorkTimeDto> GetWorkTimeDtoByUserId(int userId);
        void StartWorkingTime();
        void StopWorkingTime();
        void AddBreakTime(int minutes);
        int GetBreakTime();
    }
}