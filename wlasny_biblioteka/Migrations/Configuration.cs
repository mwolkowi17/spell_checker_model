namespace wlasny_biblioteka.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<wlasny_biblioteka.BibliotekaBaza>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "wlasny_biblioteka.BibliotekaBaza";
        }

        protected override void Seed(wlasny_biblioteka.BibliotekaBaza context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
