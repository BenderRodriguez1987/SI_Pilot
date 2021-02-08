using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Helpers
{
    public class RequestUnsuccessfulException : Exception
    {

        public RequestUnsuccessfulException(string message) : base(message)
        {
        }
    }
}
