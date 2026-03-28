using System.Data.Entity.Migrations;

namespace Common.Database.Migrations
{
    public sealed class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Libraries",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 200),
                    Address = c.String(maxLength: 500),
                    CreatedAt = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Books",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LibraryId = c.Int(nullable: false),
                    Title = c.String(nullable: false, maxLength: 300),
                    Author = c.String(nullable: false, maxLength: 200),
                    Isbn = c.String(maxLength: 13),
                    Genre = c.String(maxLength: 100),
                    PublishedYear = c.Int(),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Libraries", t => t.LibraryId, cascadeDelete: true)
                .Index(t => t.LibraryId)
                .Index(t => t.Isbn, unique: true, name: "IX_Books_Isbn");

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Username = c.String(nullable: false, maxLength: 100),
                    Email = c.String(nullable: false, maxLength: 200),
                    DisplayName = c.String(maxLength: 200),
                    Role = c.Int(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true, name: "IX_Users_Username");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Books", "LibraryId", "dbo.Libraries");
            DropIndex("dbo.Users", "IX_Users_Username");
            DropIndex("dbo.Books", "IX_Books_Isbn");
            DropIndex("dbo.Books", new[] { "LibraryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Books");
            DropTable("dbo.Libraries");
        }
    }
}
