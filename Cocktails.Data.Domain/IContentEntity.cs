using System;

namespace Cocktails.Data.Domain
{
    public interface IContentEntity
    {
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset ModifiedDate { get; set; }
    }
}
