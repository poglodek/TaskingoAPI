using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskingoAPI.Exceptions
{
    public class CannotDelete : Exception
    {
        public CannotDelete(string message) : base(message)
        {

        }

    }
}
