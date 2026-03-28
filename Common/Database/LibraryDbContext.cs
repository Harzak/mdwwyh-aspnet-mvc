using System.Data.Entity;
using Common.Database.Entities;

namespace Common.Database
{
    public sealed class LibraryDbContext : DbContext
    {
        public DbSet<Library> Libraries { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        public LibraryDbContext()
            : base("name=LibraryDbContext")
        {
        }

        public LibraryDbContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Library>()
                .HasMany(l => l.Books)
                .WithRequired(b => b.Library)
                .HasForeignKey(b => b.LibraryId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
