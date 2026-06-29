using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.Exceptions
{
    public class FailedToFindException : Exception
    {
        public FailedToFindException(string name, object key)
        : base($"Failed to Find Entity: \"{name}\" ({key}).")
        {

        }
    }
}
