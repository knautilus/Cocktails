using System;

namespace Cocktails.ViewModels
{
    public class PagingModel
    {
        public DateTimeOffset? Before { get; set; }
        public DateTimeOffset? After { get; set; }
    }
}
