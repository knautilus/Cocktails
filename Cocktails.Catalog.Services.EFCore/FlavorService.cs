using System;
using Cocktails.Catalog.ViewModels;
using Cocktails.Common.Services;
using Cocktails.Data;
using Cocktails.Data.Catalog;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Services.EFCore
{
    public class FlavorService : BaseService<Guid, Flavor, FlavorModel>
    {
        public FlavorService(IContentRepository<Guid, Flavor> repository, IModelMapper mapper)
            : base(repository, mapper) {}
    }
}
