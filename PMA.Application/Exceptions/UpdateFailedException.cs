using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.Exceptions
{
    public class UpdateFailedException : Exception
    {
        public UpdateFailedException(string name, object key)
        : base($"Failed to update Entity: \"{name}\" ({key}).")
        {

        }
    }
}
