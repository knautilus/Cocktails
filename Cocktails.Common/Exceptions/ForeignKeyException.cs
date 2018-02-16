using System;

namespace Cocktails.Common.Exceptions
{
    public class ForeignKeyException : Exception
    {
        public ForeignKeyException() { }

        public ForeignKeyException(Exception ex) : base(ex.Message, ex.InnerException)
        {
        }
    }
}
