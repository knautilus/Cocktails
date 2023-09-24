using Cocktails.Data.Core;
using Cocktails.Models.Common;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Core
{
    public abstract class GetManyInputType<TRequest> : InputObjectType<TRequest>
        where TRequest : IPagingQuery, IQuery
    {
        protected override void Configure(IInputObjectTypeDescriptor<TRequest> descriptor)
        {
            descriptor.Field(x => x.First).Type<IntType>().DefaultValue(10);
            descriptor.Field(x => x.Offset).Type<IntType>().DefaultValue(0);
        }
    }
}
