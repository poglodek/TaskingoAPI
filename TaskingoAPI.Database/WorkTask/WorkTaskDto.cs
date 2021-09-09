using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskingoAPI.Dto.User;

namespace TaskingoAPI.Dto.WorkTask
{
    public class WorkTaskDto
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } //In queue, In progress, Done, Canceled
        public string Comment { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime DeadLine { get; set; }
        public UserDto WhoCreated { get; set; }
        public bool IsAssigned { get; set; }
    }
}
