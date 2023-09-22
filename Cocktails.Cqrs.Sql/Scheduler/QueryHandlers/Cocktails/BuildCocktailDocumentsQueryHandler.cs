using AutoMapper;
using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Cqrs.Sql.DocumentBuilders;
using Cocktails.Entities.Elasticsearch;
using Cocktails.Entities.Sql;
using Cocktails.Models.Common;
using Cocktails.Models.Scheduler.Requests.Cocktails;

namespace Cocktails.Cqrs.Sql.Scheduler.QueryHandlers.Cocktails
{
    public class BuildCocktailDocumentsQueryHandler : IQueryHandler<BuildDocumentsQuery<long>, CocktailDocument[]>
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IMapper _mapper;

        public BuildCocktailDocumentsQueryHandler(IQueryProcessor queryProcessor, IMapper mapper)
        {
            _queryProcessor = queryProcessor;
            _mapper = mapper;
        }

        public async Task<CocktailDocument[]> Handle(BuildDocumentsQuery<long> query, CancellationToken cancellationToken)
        {
            var cocktails = await _queryProcessor.Process<Cocktail[]>(new CocktailGetManyQuery
            {
                CocktailIds = query.Ids,
                Take = query.Take,
                Skip = query.Skip
            }, cancellationToken);

            var recipeDocuments = await CocktailDocumentBuilder.PrepareCocktailDocuments(cocktails, _queryProcessor, _mapper, cancellationToken);

            return recipeDocuments.ToArray();
        }
    }
}
