using System;
using System.Collections.Generic;

namespace Cocktails.Api.Common
{
    internal class HttpLoggingModel
    {
        public string HttpMethod { get; set; }
        public string QueryString { get; set; }
        public Dictionary<string, IEnumerable<string>> Headers { get; set; }
        public string RequestBody { get; set; }

        public int StatusCode { get; set; }
        public string ResponseBody { get; set; }

        public DateTimeOffset StartTime { get; set; }
        public long Duration { get; set; }
    }
}
