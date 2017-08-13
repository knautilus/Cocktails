using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktails.Common.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public Guid Id { get; set; }
    }
}
