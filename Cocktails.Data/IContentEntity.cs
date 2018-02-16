using System;

namespace Cocktails.Data
{
    public interface IContentEntity
    {
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset ModifiedDate { get; set; }
    }
}
