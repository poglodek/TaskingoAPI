using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskingoAPI.Exceptions
{
    public class ConflictExceptions : Exception
    {
        public ConflictExceptions(string message) : base(message)
        {
            
        }
    }
}
