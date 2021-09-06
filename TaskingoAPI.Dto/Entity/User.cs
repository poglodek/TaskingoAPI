using System.Collections.Generic;

namespace TaskingoAPI.Database.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
        public string ActualStatus { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<WorkTask> WorkTasks { get; set; }
        public virtual List<WorkTime> WorkTimes { get; set; }
    }
}
