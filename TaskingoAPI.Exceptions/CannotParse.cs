using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskingoAPI.Exceptions
{
    public class CannotParse : Exception
    {
        public CannotParse(string message) : base(message)
        {

        }

    }
}
