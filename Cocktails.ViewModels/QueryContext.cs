﻿using System;

namespace Cocktails.ViewModels
{
    public class QueryContext
    {
        public bool IsSortAsc { get; set; } = false;
        public string Before { get; set; } = string.Empty;
        public string After { get; set; } = string.Empty;
        public int Count { get; set; } = 10;

        public DateTimeOffset? BeforeDate
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Before))
                {
                    if (long.TryParse(Before, out long longCursor))
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
                    if (long.TryParse(After, out long longCursor))
                    {
                        return new DateTimeOffset(longCursor, TimeSpan.Zero);
                    }
                }
                return null;
            }
        }
    }
}
