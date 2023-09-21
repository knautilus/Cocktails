using AutoMapper;
using Cocktails.Cqrs.Sql.DocumentBuilders;
using Cocktails.Entities.Elasticsearch;
using Cocktails.Entities.Sql;
using Cocktails.Models.Common;
using Cocktails.Models.Scheduler.Requests.Cocktails;
using MediatR;

namespace Cocktails.Cqrs.Sql.Scheduler.QueryHandlers.Cocktails
{
    public class BuildCocktailDocumentsQueryHandler : IRequestHandler<BuildDocumentsQuery<long, CocktailDocument>, CocktailDocument[]>
    {
        private readonly IMediator _queryProcessor;
        private readonly IMapper _mapper;

        public BuildCocktailDocumentsQueryHandler(IMediator queryProcessor, IMapper mapper)
        {
            _queryProcessor = queryProcessor;
            _mapper = mapper;
        }

        public async Task<CocktailDocument[]> Handle(BuildDocumentsQuery<long, CocktailDocument> query, CancellationToken cancellationToken)
        {
            var cocktails = await _queryProcessor.Send<Cocktail[]>(new CocktailGetManyQuery
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
