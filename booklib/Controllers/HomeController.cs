using AutoMapper;
using booklib.Entities;
using booklib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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


        
        public IActionResult Index()
        {
            return View();
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

            return View(model);

        }
    }
}