using BookSaw.Models.Base;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace BookSaw.Models
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }
        public Book Book  { get; set; }
        public int BookId { get; set; }
    }
}
