using System;
using System.Collections.Generic;
using System.Linq;

namespace Cocktails.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public IEnumerable<string> Errors { get; private set; }

        public BadRequestException(string message) : base(message)
        {
            Errors = new string[] { message };
        }
        public BadRequestException(IEnumerable<string> messages) : base(messages.FirstOrDefault())
        {
            Errors = messages;
        }
    }
}
