﻿using System.Collections.Generic;
using TaskingoAPI.Dto.WorkTask;

namespace TaskingoAPI.Services.IRepositories
{
    public interface IWorkTaskServices
    {
        int CreateNewTask(WorkTaskCreatedDto workTaskCreatedDto);
        void DeleteTaskById(int id);
        List<WorkTaskDto> GetTaskByMonth(int month, int year);
    }
}