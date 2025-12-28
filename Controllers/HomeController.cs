using Microsoft.AspNetCore.Mvc;
using BookSaw.DAL;
using BookSaw.Models;

namespace BookSaw.Controllers
{
    public class HomeController : Controller
    {
        BookSawDBContext _context;
        public HomeController(BookSawDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Book> books = _context.Books.ToList();
            return View(books);
        }

        public IActionResult Detail(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            return View(book);
        }
    }
}
 