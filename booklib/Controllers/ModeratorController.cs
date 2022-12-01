using AutoMapper;
using booklib.Entities;
using booklib.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace booklib.Controllers
{
    public class ModeratorController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;

        public ModeratorController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(BookModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if(_databaseContext.Books.Any(x => x.BookName.ToLower() == model.BookName.ToLower() && x.Author.ToLower() == model.Author.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.BookName), "Kitap mevcut, Stok arttırımı yapabilirsiniz.");
                    return View(model);
                }
                Book book = new Book()
                {
                    BookName = model.BookName,
                    Author = model.Author,
                    BookType = model.BookType,
                    BookImageFileName = model.BookImageFileName,
                    BookSubject = model.BookSubject,
                    PublishingDate = model.PublishingDate,
                    Stock = model.Stock
                };

                string fileName = $"p_{book.BookName}.jpg";

                Stream stream = new FileStream($"wwwroot/uploads/{fileName}", FileMode.OpenOrCreate);

                file.CopyTo(stream);
                stream.Close();
                stream.Dispose();
                book.BookImageFileName = fileName;

                _databaseContext.Books.Add(book);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "Kitap eklenememiştir.");
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        //[HttpPost]
        //public IActionResult BookImage([Required] IFormFile file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Guid bookid = new Guid(Book.FindFirstValue(ClaimTypes.NameIdentifier));
        //        Book book = _databaseContext.Books.SingleOrDefault(x => x.BookId == bookid);

        //        string fileName = $"p_{bookid}.jpg";

        //        Stream stream = new FileStream($"wwwroot/uploads/{fileName}", FileMode.OpenOrCreate);

        //        file.CopyTo(stream);
        //        stream.Close();
        //        stream.Dispose();

        //        book.BookImageFileName = fileName;
        //        _databaseContext.SaveChanges();

        //        return RedirectToAction(nameof(Index));


        //    }
        //    return View(nameof(Index));
        //}
    }
}
