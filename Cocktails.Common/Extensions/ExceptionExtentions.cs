using System;
using System.Linq;

namespace Cocktails.Common.Extensions
{
    public static class ExceptionExtentions
    {
        public static bool Contains(this Exception exception, string text)
        {
            return exception.GetHierarchy(x => x.InnerException)
                .Any(e => e.Message.IndexOf(text, 0, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public static Exception GetMostInner(this Exception exception)
        {
            return exception.GetHierarchy(x => x.InnerException)
                .LastOrDefault();
        }
    }
}
