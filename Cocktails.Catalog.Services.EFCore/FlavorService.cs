using System;
using Cocktails.Catalog.ViewModels;
using Cocktails.Data;
using Cocktails.Data.Domain;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Services.EFCore
{
    public class FlavorService : BaseService<Flavor, FlavorModel>
    {
        public FlavorService(IContentRepository<Guid, Flavor> repository, IModelMapper mapper)
            : base(repository, mapper) {}
    }
}
