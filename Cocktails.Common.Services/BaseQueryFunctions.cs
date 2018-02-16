using System;
using System.Linq;
using Cocktails.Data;

namespace Cocktails.Common.Services
{
    public class BaseQueryFunctions
    {
        public static Func<IQueryable<T>, Guid, IQueryable<T>> GetByIdFunction<T>() where T : BaseEntity<Guid> =>
            (x, id) => x.Where(y => y.Id == id);
    }
}
