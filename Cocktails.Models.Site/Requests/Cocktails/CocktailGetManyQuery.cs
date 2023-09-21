﻿using Cocktails.Entities.Elasticsearch;
using Cocktails.Models.Common;

namespace Cocktails.Models.Site.Requests.Cocktails
{
    public class CocktailGetManyQuery : GetManyQuery<CocktailDocument, CocktailSort>
    {
    }
}
