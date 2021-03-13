namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ConsertoDeLivro.Models.ConsertoDeLivroContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ConsertoDeLivro.Models.ConsertoDeLivroContext";
        }

        protected override void Seed(ConsertoDeLivro.Models.ConsertoDeLivroContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
