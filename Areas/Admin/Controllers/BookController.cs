using BookSaw.DAL;
using Microsoft.AspNetCore.Mvc;
using BookSaw.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using BookSaw.Areas.Admin.ViewModels.Book;

namespace BookSaw.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController(BookSawDBContext context, IWebHostEnvironment env) : Controller
    {
        private readonly BookSawDBContext _context = context;
        private readonly IWebHostEnvironment _env = env;

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.Include(x => x.Images).Include(x => x.Categories).ToListAsync();
            return View(books);
        }

        //public IActionResult Index()
        //{
        //    var books = _context.Books.ToList();

        //    if (books != null)
        //    {
        //        return View(books);
        //    }

        //    return View("Null data gonderildi - xeta bas verdi!");
            
        //}

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                DiscountRate = model.DiscountRate,
                ISBN = model.ISBN,
                Description = model.Description,
                Pages = model.Pages,
                Publisher = model.Publisher,
                Language = model.Language,
                Price = model.Price,
                Categories = _context.Categories.Where(c => model.CategoryIds.Contains(c.Id)).ToList()
            };

            foreach ( var img in model.Images)
            {
                string fileName = Guid.NewGuid() + "_" + img.FileName;
                string path = Path.Combine(_env.WebRootPath, "Upload", "Book");
                string fullPath = Path.Combine(path, fileName);
                
                await using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }
                
                book.Images?.Add(new Image()
                {
                    Path=fileName
                });
               
            }
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
