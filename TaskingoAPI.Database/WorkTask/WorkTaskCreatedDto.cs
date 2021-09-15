using System;

namespace TaskingoAPI.Dto.WorkTask
{
    public class WorkTaskCreatedDto
    {
        public int Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string WorkGroup { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
