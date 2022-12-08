using AutoMapper;
using booklib.Entities;
using booklib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace booklib.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

               public HomeController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
                {
                    _databaseContext = databaseContext;
                    _configuration = configuration;
                    _mapper = mapper;
                }

        public async Task<IActionResult> Index(string SearchString)
        {
            //ViewData["CurrentFilter"] = SearchString;

            var books = _databaseContext.Books.ToList()
                .Select(x => _mapper.Map<BookSearchModel>(x)).Where(x => x.BookName.ToLower() == SearchString).ToList();

            //var books = from b in _databaseContext.Books select b;
            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //    books = books.Where(b => b.BookName.Contains(SearchString) || b.Author.Contains(SearchString));
            //}

            return View(books);
        }       

              
        [Authorize(Roles = "user, admin, moderator")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DetailsList(BookModel model)
        {

            List<BookModel> books=_databaseContext.Books.ToList().Select(x=>_mapper.Map<BookModel>(x)).ToList();

            return View(books);

        }
        
        public IActionResult BookDetails(Guid id, BookModel model)
        {            
            Book book = _databaseContext.Books.Find(id);
            //model.BookImageFileName = book.BookImageFileName;
            _mapper.Map(book, model);
            return View(model);
        } 
    }
}