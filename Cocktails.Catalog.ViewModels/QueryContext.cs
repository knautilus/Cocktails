using System;

namespace Cocktails.Catalog.ViewModels
{
    public class QueryContext
    {
        public SortMode Sort { get; set; } = SortMode.Desc;
        public string Before { get; set; } = string.Empty;
        public string After { get; set; } = string.Empty;
        public int Limit { get; set; } = 10;

        public DateTimeOffset? BeforeDate
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Before))
                {
                    if (long.TryParse(Before, out var longCursor))
                    {
                        return new DateTimeOffset(longCursor, TimeSpan.Zero);
                    }
                }
                return null;
            }
        }

        public DateTimeOffset? AfterDate
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(After))
                {
                    if (long.TryParse(After, out var longCursor))
                    {
                        return new DateTimeOffset(longCursor, TimeSpan.Zero);
                    }
                }
                return null;
            }
        }
    }
}
