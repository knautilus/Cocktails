using System;
using System.Collections.Generic;
using System.Linq;

namespace Cocktails.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public BadRequestException(string message) : base(message)
        {
            Errors = new[] { message };
        }

        public BadRequestException(ICollection<string> messages) : base(messages.FirstOrDefault())
        {
            Errors = messages;
        }
    }
}
