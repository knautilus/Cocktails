using System;

namespace Cocktails.Common.Objects
{
    public class ApiInfo
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTimeOffset ServerTime => DateTimeOffset.UtcNow;
    }
}
