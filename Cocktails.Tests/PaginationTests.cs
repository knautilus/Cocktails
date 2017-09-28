using System;
using System.Linq;

using NUnit.Framework;

using Cocktails.Catalog.Services.EFCore;
using Cocktails.Catalog.ViewModels;

namespace Cocktails.Tests
{
    public class PaginationTests : DbContextTestBase
    {
        [Test]
        public void PaginateAscBeforeReturnsLesserValues()
        {
            var rand = new Random();

            var source = CocktailsContext.Ingredients.AsQueryable();
            var index = rand.Next(source.Count() - 1);
            var limit = rand.Next(source.Count() - 1);
            var before = source.ToArray()[index].ModifiedDate;
            var queryContext = new QueryContext
            {
                Before = before.Ticks.ToString(),
                Sort = SortMode.Asc,
                Limit = limit
            };

            var result = source.Paginate(queryContext, x => x.ModifiedDate).ToList();

            Assert.NotNull(result);
            Assert.LessOrEqual(result.Count, limit);
            result.ForEach(x => Assert.Less(x.ModifiedDate, before));
        }

        [Test]
        public void PaginateAscBeforeMaxDateReturnsListWithLimitSize()
        {
            var rand = new Random();

            var source = CocktailsContext.Ingredients.AsQueryable();
            var limit = rand.Next(source.Count() - 1);
            var before = DateTimeOffset.MaxValue;
            var queryContext = new QueryContext
            {
                Before = before.Ticks.ToString(),
                Sort = SortMode.Asc,
                Limit = limit
            };

            var result = source.Paginate(queryContext, x => x.ModifiedDate).ToList();

            Assert.NotNull(result);
            Assert.AreEqual(result.Count, limit);
            result.ForEach(x => Assert.Less(x.ModifiedDate, before));
        }

        [Test]
        public void PaginateAscAfterReturnsGreaterValues()
        {
            var rand = new Random();

            var source = CocktailsContext.Ingredients.AsQueryable();
            var index = rand.Next(source.Count() - 1);
            var limit = rand.Next(source.Count() - 1);
            var after = source.ToArray()[index].ModifiedDate;
            var queryContext = new QueryContext
            {
                After = after.Ticks.ToString(),
                Sort = SortMode.Asc,
                Limit = limit
            };

            var result = source.Paginate(queryContext, x => x.ModifiedDate).ToList();

            Assert.NotNull(result);
            Assert.LessOrEqual(result.Count, limit);
            result.ForEach(x => Assert.Greater(x.ModifiedDate, after));
        }

        [Test]
        public void PaginateAscAfterMinDateReturnsListWithLimitSize()
        {
            var rand = new Random();

            var source = CocktailsContext.Ingredients.AsQueryable();
            var limit = rand.Next(source.Count() - 1);
            var after = DateTimeOffset.MinValue;
            var queryContext = new QueryContext
            {
                After = after.Ticks.ToString(),
                Sort = SortMode.Asc,
                Limit = limit
            };

            var result = source.Paginate(queryContext, x => x.ModifiedDate).ToList();

            Assert.NotNull(result);
            Assert.AreEqual(result.Count, limit);
            result.ForEach(x => Assert.Greater(x.ModifiedDate, after));
        }

        [Test]
        public void PaginateDescBeforeReturnsGreaterValues()
        {
            var rand = new Random();

            var source = CocktailsContext.Ingredients.AsQueryable();
            var index = rand.Next(source.Count() - 1);
            var limit = rand.Next(source.Count() - 1);
            var before = source.ToArray()[index].ModifiedDate;
            var queryContext = new QueryContext
            {
                Before = before.Ticks.ToString(),
                Sort = SortMode.Desc,
                Limit = limit
            };

            var result = source.Paginate(queryContext, x => x.ModifiedDate).ToList();

            Assert.NotNull(result);
            Assert.LessOrEqual(result.Count, limit);
            result.ForEach(x => Assert.Greater(x.ModifiedDate, before));
        }

        [Test]
        public void PaginateDescBeforeMinDateReturnsListWithLimitSize()
        {
            var rand = new Random();

            var source = CocktailsContext.Ingredients.AsQueryable();
            var limit = rand.Next(source.Count() - 1);
            var before = DateTimeOffset.MinValue;
            var queryContext = new QueryContext
            {
                Before = before.Ticks.ToString(),
                Sort = SortMode.Desc,
                Limit = limit
            };

            var result = source.Paginate(queryContext, x => x.ModifiedDate).ToList();

            Assert.NotNull(result);
            Assert.AreEqual(result.Count, limit);
            result.ForEach(x => Assert.Greater(x.ModifiedDate, before));
        }

        [Test]
        public void PaginateDescAfterReturnsLesserValues()
        {
            var rand = new Random();

            var source = CocktailsContext.Ingredients.AsQueryable();
            var index = rand.Next(source.Count() - 1);
            var limit = rand.Next(source.Count() - 1);
            var after = source.ToArray()[index].ModifiedDate;
            var queryContext = new QueryContext
            {
                After = after.Ticks.ToString(),
                Sort = SortMode.Desc,
                Limit = limit
            };

            var result = source.Paginate(queryContext, x => x.ModifiedDate).ToList();

            Assert.NotNull(result);
            Assert.LessOrEqual(result.Count, limit);
            result.ForEach(x => Assert.Less(x.ModifiedDate, after));
        }

        [Test]
        public void PaginateDescAfterMaxDateReturnsListWithLimitSize()
        {
            var rand = new Random();

            var source = CocktailsContext.Ingredients.AsQueryable();
            var limit = rand.Next(source.Count() - 1);
            var after = DateTimeOffset.MaxValue;
            var queryContext = new QueryContext
            {
                After = after.Ticks.ToString(),
                Sort = SortMode.Desc,
                Limit = limit
            };

            var result = source.Paginate(queryContext, x => x.ModifiedDate).ToList();

            Assert.NotNull(result);
            Assert.AreEqual(result.Count, limit);
            result.ForEach(x => Assert.Less(x.ModifiedDate, after));
        }
    }
}
