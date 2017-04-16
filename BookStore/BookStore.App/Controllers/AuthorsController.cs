using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using BookStore.Data;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Services;
using System.Collections.Generic;
using BookStore.Models.EntityModels;

namespace BookStore.App.Controllers
{
    public class AuthorsController : Controller
    {
        private AuthorService authorService;

        public AuthorsController()
        {
            this.authorService = new AuthorService();
        }

        // GET: Authors
        [AllowAnonymous]
        public ActionResult All()
        {
            IEnumerable<AuthorViewModel> viewModel = this.authorService.GetAll();
            return View(viewModel);
        }

        // GET: Authors/Details/id
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                this.TempData["Error"] = "Invalid url.";
                return RedirectToAction("All", "Authors");
            }

            AuthorViewModel viewModel = this.authorService.GetAuthor(id);
            if (viewModel == null)
            {
                this.TempData["Error"] = $"There is no author whit id: {id}";
                return RedirectToAction("All", "Authors");
            }

            return View(viewModel);
        }

        // GET: Authors/BooksByAuthor?authorName=...
        [ActionName("BooksByAuthor")]
        public ActionResult BooksByAuthorFullName(string authorName)
        {
            if (string.IsNullOrEmpty(authorName))
            {
                this.TempData["Error"] = "Enter author's name to find books.";
                return View();
            }

            AuthorWithBooksViewModel viewModel = this.authorService.GetAuthorWithBooks(authorName);
            if (viewModel == null)
            {
                this.TempData["Error"] = $"No author whith name: '{authorName}'";
                return View();
            }

            return View(viewModel);
        }
    }
}
