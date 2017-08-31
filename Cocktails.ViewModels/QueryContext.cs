using System;

namespace Cocktails.ViewModels
{
    public class QueryContext
    {
        public bool IsSortAsc { get; set; } = false;
        public DateTimeOffset? Before { get; set; }
        public DateTimeOffset? After { get; set; }
        public int Count { get; set; } = 10;
    }
}
