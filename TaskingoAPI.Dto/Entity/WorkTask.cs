using System;

namespace TaskingoAPI.Database.Entity
{
    public class WorkTask
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } //In queue, In progress, Done, Canceled
        public string Comment { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime DeadLine { get; set; }
        public virtual User WhoCreated { get; set; }
        public bool IsAssigned { get; set; }
    }
}
