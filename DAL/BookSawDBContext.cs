using Microsoft.EntityFrameworkCore;
using BookSaw.Models;
namespace BookSaw.DAL
{
    public class BookSawDBContext:DbContext
    {
        public BookSawDBContext(DbContextOptions<BookSawDBContext> options):base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
