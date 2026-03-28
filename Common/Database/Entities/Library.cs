using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Database.Entities
{
    [Table("Libraries")]
    public sealed class Library : IEquatable<Library>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Book> Books { get; set; }

        public Library()
        {
            this.Books = new List<Book>();
            this.CreatedAt = DateTime.UtcNow;
        }

        public bool Equals(Library other)
        {
            if (other is null)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Library);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Library [Id={this.Id}, Name={this.Name}]";
        }
    }
}
