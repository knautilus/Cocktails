using AutoMapper;
using Cocktails.Entities.Elasticsearch;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests;
using Cocktails.Models.Cms.Requests.Mixes;
using Cocktails.Models.Common;
using MediatR;

namespace Cocktails.Cqrs.Sql.DocumentBuilders
{
    public class CocktailDocumentBuilder
    {
        public static async Task<CocktailDocument[]> PrepareCocktailDocuments(Cocktail[] cocktails,
            IMediator queryProcessor, IMapper mapper, CancellationToken cancellationToken)
        {
            var cocktailDocuments = new List<CocktailDocument>();

            var flavorIds = cocktails
                .Select(x => x.FlavorId)
                .Distinct().ToArray();
            var flavors = await queryProcessor.Send(new GetByIdsQuery<long, Flavor> { Ids = flavorIds }, cancellationToken);
            var flavorDictionary = flavors.ToDictionary(x => x.Id);

            var cocktailCategoryIds = cocktails
                .Select(x => x.CocktailCategoryId)
                .Distinct().ToArray();
            var cocktailCategories = await queryProcessor.Send(new GetByIdsQuery<long, CocktailCategory> { Ids = cocktailCategoryIds }, cancellationToken);
            var cocktailCategoryDictionary = cocktailCategories.ToDictionary(x => x.Id);

            var cocktailsIds = cocktails
                .Select(x => x.Id)
                .Distinct().ToArray();
            var mixes = await queryProcessor.Send(new MixesGetByCocktailIdsQuery { CocktailIds = cocktailsIds }, cancellationToken);
            var mixLookup = mixes.ToLookup(x => x.CocktailId);

            var ingredientIds = mixes
                .Select(x => x.IngredientId)
                .Distinct().ToArray();
            var ingredients = await queryProcessor.Send(new GetByIdsQuery<long, Ingredient> { Ids = ingredientIds }, cancellationToken);
            var ingredientDictionary = ingredients.ToDictionary(x => x.Id);

            var measureUnitIds = mixes
                .Select(x => x.MeasureUnitId)
                .Distinct().ToArray();
            var measureUnits = await queryProcessor.Send(new GetByIdsQuery<long, MeasureUnit> { Ids = measureUnitIds }, cancellationToken);
            var measureUnitDictionary = measureUnits.ToDictionary(x => x.Id);

            foreach (var cocktail in cocktails)
            {
                var document = mapper.Map<CocktailDocument>(cocktail);

                document.Flavor = mapper.Map<FlavorDocument>(flavorDictionary[cocktail.FlavorId]);
                document.CocktailCategory = mapper.Map<CocktailCategoryDocument>(cocktailCategoryDictionary[cocktail.CocktailCategoryId]);

                var mixDocuments = PrepareMixDocuments(mixLookup, cocktail, measureUnitDictionary, ingredientDictionary, mapper);

                document.Mixes = mixDocuments;

                cocktailDocuments.Add(document);
            }

            return cocktailDocuments.ToArray();
        }

        private static MixDocument[] PrepareMixDocuments(ILookup<long, Mix> mixLookup, Cocktail cocktail, Dictionary<long, MeasureUnit> measureUnitDictionary,
            Dictionary<long, Ingredient> ingredientDictionary, IMapper mapper)
        {
            var mixDocuments = new List<MixDocument>();
            foreach (var mix in mixLookup[cocktail.Id])
            {
                var mixDocument = mapper.Map<MixDocument>(mix);
                mixDocument.MeasureUnit = mapper.Map<MeasureUnitDocument>(measureUnitDictionary[mix.MeasureUnitId]);
                mixDocument.Ingredient = mapper.Map<IngredientDocument>(ingredientDictionary[mix.IngredientId]);

                mixDocuments.Add(mixDocument);
            }

            return mixDocuments.ToArray();
        }
    }
}
