using BookSaw.Models;
using BookSaw.Models.Base;

namespace BookSaw.Areas.Admin.ViewModels.Book
{
    public class CreateViewModel
    {
            public string ImageUrl { get; set; }

            public required string Title { get; set; }
            
            public string Author { get; set; }
           
            public decimal Price { get; set; }
          
            public string Description { get; set; }
            public decimal DiscountRate { get; set; }
         
            public string ISBN { get; set; }
        public string Publisher { get; set; }
            public string Language { get; set; }
       
            public int Pages { get; set; }
            //public List<Image?> Images { get; set; }
            public List<int> CategoryIds { get; set; }
            public List<IFormFile> Images { get; set; }
    }
}
