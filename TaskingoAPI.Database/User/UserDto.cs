using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskingoAPI.Database.Entity;
using TaskingoAPI.Dto.Entity;

namespace TaskingoAPI.Dto.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ActualStatus { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
    }
}
