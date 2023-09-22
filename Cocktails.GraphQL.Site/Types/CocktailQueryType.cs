using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Elasticsearch;
using Cocktails.GraphQL.Site.Queries;
using Cocktails.Models.Site.Requests.Cocktails;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;

namespace Cocktails.GraphQL.Site.Types
{
    public class CocktailQueryType : ObjectTypeExtension<CocktailQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<CocktailQuery> descriptor)
        {
            descriptor.Field(x => x.GetCocktails(default, default, default))
                .UsePaging(options: new PagingOptions { IncludeTotalCount = true })
                .Argument("request", x => x.Type<CocktailGetManyInputType>())
                .Use(next => async context =>
                {
                    await next(context);

                    var request = context.ArgumentValue<CocktailGetManyQuery>("request");

                    var data = context.Result as CocktailDocument[];

                    var edges = data.Select(x => new Edge<CocktailDocument>(x, x.Id.ToString()))
                        .ToList();

                    var pageInfo = new ConnectionPageInfo(false, false, null, null);

                    var queryProcessor = context.Service<IQueryProcessor>();

                    context.Result = new Connection<CocktailDocument>(edges, pageInfo, async ct => await queryProcessor.Process<int>(request, ct));
                });

            descriptor.Field(x => x.GetCocktailsCount(default, default, default));

            //descriptor.Field(x => x.GetCocktails(default, default, default))
            //    .UsePaging(options: new PagingOptions { IncludeTotalCount = true })
            //    .Argument("request", x => x.Type<CocktailGetManyInputType>())
            //    .Resolve(async context =>
            //    {
            //        //var skip = context.ArgumentValue<int>("skip");
            //        var take = context.ArgumentValue<int>("first");

            //        var request = context.ArgumentValue<CocktailGetManyQuery>("request");
            //        request.Skip = 0;
            //        request.Take = take;

            //        var data = await context.Parent<CocktailQuery>().GetCocktails(request, context.Service<IMediator>(), new CancellationToken());
            //        var edges = data.Select(x => new Edge<CocktailDocument>(x, x.Id.ToString()))
            //            .ToList();
            //        var pageInfo = new ConnectionPageInfo(false, false, null, null);

            //        var connection = new Connection<CocktailDocument>(edges, pageInfo,
            //            async ct => await context.Parent<CocktailQuery>().GetCocktailsCount(request, context.Service<IMediator>(), ct));

            //        return connection;
            //    })
            //    .Type<ListType<CocktailType>>();

            //descriptor.Field("cocks")
            //    .UsePaging()
            //    .Resolve(async context =>
            //    {
            //        var data = await context.Parent<CocktailQuery>().GetCocktails(context.ArgumentValue<CocktailGetManyQuery>("request"), context.Service<IMediator>(), new CancellationToken());
            //        var edges = data.Select(x => new Edge<CocktailDocument>(x, x.Id.ToString()))
            //            .ToList();
            //        var pageInfo = new ConnectionPageInfo(false, false, null, null);

            //        var connection = new Connection<CocktailDocument>(edges, pageInfo,
            //            async ct => await context.Parent<CocktailQuery>().GetCocktailsCount(context.ArgumentValue<CocktailGetManyQuery>("request"), context.Service<IMediator>(), ct));

            //        return connection;
            //    });

            //descriptor.Field(t => t.GetCocktail(default, default, default))
            //    .Type<CocktailType>();
        }
    }
}
