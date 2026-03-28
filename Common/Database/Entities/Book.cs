using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Database.Entities
{
    [Table("Books")]
    public sealed class Book : IEquatable<Book>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int LibraryId { get; set; }

        [ForeignKey("LibraryId")]
        public Library Library { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Author { get; set; }

        [MaxLength(13)]
        [Index("IX_Books_Isbn", IsUnique = true)]
        public string Isbn { get; set; }

        [MaxLength(100)]
        public string Genre { get; set; }

        public int? PublishedYear { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Book()
        {
            DateTime now = DateTime.UtcNow;
            this.CreatedAt = now;
            this.UpdatedAt = now;
        }

        public bool Equals(Book other)
        {
            if (other is null)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Book);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Book [Id={this.Id}, Title={this.Title}, Author={this.Author}]";
        }
    }
}
