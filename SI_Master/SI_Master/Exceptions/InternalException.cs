using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Exceptions
{
    class InternalException : Exception
    {
        public InternalException(string message) : base(message)
        {

        }
    }
}
