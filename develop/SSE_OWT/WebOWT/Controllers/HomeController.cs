using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Linq;
using WebOWT.Models;
using WebOWT.Models.EntityDataModels;
using WebOWT.Services;

namespace WebOWT.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IBookService _bookService;

        public HomeController(IConfiguration config, ITokenService tokenService, IBookService bookService)
        {
            _config = config;
            _tokenService = tokenService;
            _bookService = bookService;
        }

        [Authorize]
        [Route("mainwindow")]
        [HttpGet]
        public IActionResult MainWindow(string searchKey)
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Index", "Login"));
            }

            if (!_tokenService.IsTokenValid(_config["Jwt:Key"].ToString(),
                _config["Jwt:Issuer"].ToString(), token))
            {
                return (RedirectToAction("Index"));
            }

            var books = _bookService.GetAll();

            ViewData["SearchKey"] = searchKey;

            if (!string.IsNullOrEmpty(searchKey))
            {
                searchKey = searchKey.ToLower();

                books = books.Where(f => f.Title.ToLower().Contains(searchKey) || f.Author.ToLower().Contains(searchKey) || f.Description.ToLower().Contains(searchKey));
            }

            return View(books.ToList());
        }

        [Authorize]
        [Route("detail")]
        [HttpGet]
        public IActionResult BookDetail(int Id)
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Index", "Login"));
            }

            if (!_tokenService.IsTokenValid(_config["Jwt:Key"].ToString(),
                _config["Jwt:Issuer"].ToString(), token))
            {
                return (RedirectToAction("Index"));
            }

            return View(_bookService.GetById(Id));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            Book book = new Book();

            var cates = _bookService.GetAllCategory().ToList();

            cates.Insert(0, new Category { Id = 0, Title = "--Select--" });
            ViewBag.Cates = cates;

            var maxValue = _bookService.GetAll().Max(m => m.Id);
            book.Id = maxValue + 1;

            book.Owner = HttpContext.Session.GetString("CurrentUser").ToString();

            return View(book);
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bookService.Create(book);
                    return RedirectToAction("MainWindow");
                }
            }

            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. ");
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        [Route("edit")]
        public IActionResult Edit(int id)
        {
            var cates = _bookService.GetAllCategory().ToList();

            cates.Insert(0, new Category { Id = 0, Title = "--Select--" });
            ViewBag.Cates = cates;

            var book = _bookService.GetById(id);

            return View(book);
        }

        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bookService.Update(book);
                    return RedirectToAction("MainWindow");
                }
            }

            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. ");
            }
            return View();
        }

        [Authorize]
        [Route("delete")]
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            return View(_bookService.GetById(Id));
        }

        [Authorize]
        [Route("delete")]
        [HttpPost]
        public IActionResult ConfirmDelete(int Id)
        {
            _bookService.Delete(Id);
            return RedirectToAction("MainWindow");
        }
    }
}
