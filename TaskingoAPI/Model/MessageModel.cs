using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskingoAPI.Model
{
    public class MessageModel
    {
        public string Sender { get; set; }
        public string UserMessage { get; set; }

        public MessageModel(string sender, string userMessage)
        {
            Sender = sender;
            UserMessage = userMessage;
        }
    }
}
