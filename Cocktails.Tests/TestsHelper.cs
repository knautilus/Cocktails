using System;
using System.Collections.Generic;
using System.Text;
using Cocktails.Data.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cocktails.Tests
{
    public class TestsHelper
    {
        public static DbContextOptions<CocktailsContext> CreateContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<CocktailsContext>();
            builder.UseInMemoryDatabase("cocktailsdb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
