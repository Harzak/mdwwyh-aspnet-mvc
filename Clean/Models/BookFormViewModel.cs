using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clean.Models
{
    public sealed class BookFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(300, ErrorMessage = "Title cannot exceed 300 characters.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [MaxLength(200, ErrorMessage = "Author cannot exceed 200 characters.")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [MaxLength(13, ErrorMessage = "ISBN cannot exceed 13 characters.")]
        [Display(Name = "ISBN")]
        public string Isbn { get; set; }

        [MaxLength(100, ErrorMessage = "Genre cannot exceed 100 characters.")]
        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Display(Name = "Published Year")]
        [Range(1, 9999, ErrorMessage = "Published year must be between 1 and 9999.")]
        public int? PublishedYear { get; set; }

        [Required(ErrorMessage = "Library is required.")]
        [Display(Name = "Library")]
        public int LibraryId { get; set; }

        public List<LibrarySelectItemViewModel> AvailableLibraries { get; set; }

        public BookFormViewModel()
        {
            this.AvailableLibraries = new List<LibrarySelectItemViewModel>();
        }
    }
}
