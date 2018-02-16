using System;

namespace Cocktails.Common.Exceptions
{
    public class PrimaryKeyException : Exception
    {
        public PrimaryKeyException() { }

        public PrimaryKeyException(Exception ex) : base(ex.Message, ex.InnerException)
        {
        }
    }
}
