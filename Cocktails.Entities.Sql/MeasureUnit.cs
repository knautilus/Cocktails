using Cocktails.Entities.Common;
using System.Collections.Generic;

namespace Cocktails.Entities.Sql
{
    public class MeasureUnit : BaseContentEntity<long>
    {
        public string Name { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
