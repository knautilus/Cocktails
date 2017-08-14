using System;

namespace Cocktails.Common.Models
{
    public class ApiInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public DateTimeOffset ServerTime => DateTimeOffset.UtcNow;
    }
}
