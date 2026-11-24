using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [Display(Name = "Название")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Автор обязателен")]
        [Display(Name = "Автор")]
        [StringLength(200)]
        public string Author { get; set; } = string.Empty;

        [Display(Name = "Год издания")]
        [Range(1000, 2100, ErrorMessage = "Введите корректный год")]
        public int? PublicationYear { get; set; }

        [Display(Name = "Оглавление")]
        public string? Contents { get; set; }

        [Display(Name = "Дата добавления")]
        public DateTime AddedDate { get; set; }

        [Display(Name = "Жанр")] 
        public string? Genre { get; set; } = string.Empty;
    }
}