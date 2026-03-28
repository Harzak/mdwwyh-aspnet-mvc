using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Database.Enums;

namespace Common.Database.Entities
{
    [Table("Users")]
    public sealed class User : IEquatable<User>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index("IX_Users_Username", IsUnique = true)]
        public string Username { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string DisplayName { get; set; }

        [Required]
        public EUserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public User()
        {
            this.Role = EUserRole.Reader;
            this.CreatedAt = DateTime.UtcNow;
        }

        public bool Equals(User other)
        {
            if (other is null)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as User);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"User [Id={this.Id}, Username={this.Username}, Role={this.Role}]";
        }
    }
}
