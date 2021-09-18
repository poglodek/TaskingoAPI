using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskingoAPI.Database.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
        public string UserIdChat { get; set; }
        public int Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsOnline { get; set; }
        public string Address { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<WorkTask> WorkTasks { get; set; }
        public virtual List<WorkTask> WorkTasksAssigned { get; set; }
        public virtual List<WorkTime> WorkTimes { get; set; }
        public virtual List<Message> Sender { get; set; }
        public virtual List<Message> Recipient { get; set; }
    }
}
