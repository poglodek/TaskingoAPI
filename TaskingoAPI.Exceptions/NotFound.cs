﻿using System;

namespace TaskingoAPI.Exceptions
{
    public class NotFound : Exception
    {
        public NotFound(string message) : base(message)
        {

        }

    }
}
