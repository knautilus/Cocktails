using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktails.Data.Domain
{
    public class Cocktail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Mix> Mixes { get; set; }
    }
}
