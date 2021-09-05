using System;

namespace TaskingoAPI.Exceptions
{
    public class CannotDelete : Exception
    {
        public CannotDelete(string message) : base(message)
        {

        }

    }
}
