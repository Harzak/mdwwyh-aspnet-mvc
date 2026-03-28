using System.Data.Entity.Migrations;

namespace Common.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LibraryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations";
        }
    }
}
