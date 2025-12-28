using BookSaw.Models.Base;
using System.ComponentModel.DataAnnotations;
namespace BookSaw.Models
{
    public class Book : BaseEntity
    {
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal DiscountRate { get; set; }
        [Required]
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        [Required]
        public int Pages { get; set; }
        public List<Image?> Images { get; set; }
        public List<Category> Categories { get; set; }

    }
}
