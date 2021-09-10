using System;

namespace TaskingoAPI.Database.Entity
{
    public class WorkTime
    {
        public int Id { get; set; }
        public DateTime WorkTimeStart { get; set; }
        public DateTime? WorkTimeEnd { get; set; }
        public int BreakTimeInMinutes { get; set; }
        public virtual User User { get; set; }
    }
}
