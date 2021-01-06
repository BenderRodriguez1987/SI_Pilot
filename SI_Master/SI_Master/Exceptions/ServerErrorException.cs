using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Exceptions
{
    class ServerErrorException : Exception
    {
        public ServerErrorException(string message) : base(message)
        {

        }
    }
}
