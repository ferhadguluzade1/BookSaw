using BookSaw.DAL;
using Microsoft.AspNetCore.Mvc;
using BookSaw.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace BookSaw.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly BookSawDBContext _context;

        public BookController(BookSawDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.ToList();

            if (books != null)
            {
                return View(books);
            }

            return View("Null data gonderildi - xeta bas verdi!");
            
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            List<Book>? books = await _context.Books.Include(x=>x.Images).ToListAsync();
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Gozlenilmez xeta - NULL");
        }

        public IActionResult Edit(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                return View(book);
            }
            return View("Gozlenilmez xeta bas verdi - null");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            var oldBook = _context.Books.FirstOrDefault(x => x.Id == book.Id);
            oldBook.Title = book.Title;
            oldBook.Author = book.Author;
            oldBook.ImageUrl = book.ImageUrl;
            oldBook.Price = book.Price;
            oldBook.Description = book.Description;
            oldBook.Id = book.Id;
            _context.Books.Update(oldBook);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
