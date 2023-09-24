using Cocktails.Data.EFCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cocktails.DbMigrations
{
    public class LegacyContextFactory : IDesignTimeDbContextFactory<CocktailsContext>
    {
        public CocktailsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CocktailsContext>();
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;encrypt=False;",
                b => b.MigrationsAssembly("Cocktails.DbMigrations"));
            //optionsBuilder.UseSqlServer(@"Data Source=tcp:AGL-001.afishawin.rambler.tech,24331;MultiSubnetFailover=true;Initial Catalog=ANGE_Eda;Connection Timeout=240;uid=eda_ddl_user;password=***;enlist=false;Application Name=neweda",
            //    b => b.MigrationsAssembly("Eda.DbMigrations"));

            return new CocktailsContext(optionsBuilder.Options);
        }
    }
}
