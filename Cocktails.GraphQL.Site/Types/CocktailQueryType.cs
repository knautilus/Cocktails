using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Common;
using Cocktails.Entities.Elasticsearch;
using Cocktails.GraphQL.Core;
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
                .Argument("request", x => x.Type<CocktailGetManyInputType>())
                .UseCustomPaging<long, CocktailDocument, CocktailGetManyQuery>();

            //descriptor.Field(x => x.GetCocktails(default, default, default))
            //    .UsePaging(options: new PagingOptions { IncludeTotalCount = true })
            //    .Argument("request", x => x.Type<CocktailGetManyInputType>())
            //    .Resolve(async context =>
            //    {
            //        //var skip = context.ArgumentValue<int>("skip");
            //        var take = context.ArgumentValue<int>("first");

            //        var request = context.ArgumentValue<CocktailGetManyQuery>("request");
            //        request.Offset = 0;
            //        request.First = take;

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

            descriptor.Field(t => t.GetCocktail(default, default, default))
                .Type<CocktailType>();
        }

        //private IReadOnlyList<CollectionEdge<CocktailDocument>> GetSelectedEdges(CocktailDocument[] source)
        //{
        //    var list = new List<CollectionEdge<CocktailDocument>>();

        //    var edges = source.Select(x => new Edge<CocktailDocument>(x, x.Id.ToString())).ToArray();

        //    for (int i = 0; i < edges.Length; i++)
        //    {
        //        string cursor = "JsonConvert.SerializeObject(_properties).GetHashCode().ToString()"; // TODO base64 encode?
        //        list.Add(new CollectionEdge<CocktailDocument>(cursor, edges[i], source.Offset + i));
        //    }

        //    return list;
        //}
    }

    public class CollectionEdge<T>
        : Edge<T>
    {
        public CollectionEdge(string cursor, T node, int index)
            : base(node, cursor)
        {
            Index = index;
        }

        public int Index { get; set; }
    }
}
