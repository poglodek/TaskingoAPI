using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskingoAPI.Database.Entity
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public User WhoSentMessage { get; set; }
        public User WhoGotMessage { get; set; }
        public string UserMessage { get; set; }
    }
}
