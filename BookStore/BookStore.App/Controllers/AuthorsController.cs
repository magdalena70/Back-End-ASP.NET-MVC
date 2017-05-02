using System.Web.Mvc;
using System.Collections.Generic;
using BookStore.Models.ViewModels.Author;
using System;
using BookStore.Services.Interfaces;

namespace BookStore.App.Controllers
{
    public class AuthorsController : Controller
    {
        private IAuthorService authorService;

        public AuthorsController(IAuthorService service)
        {
            this.authorService = service;
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
                throw new Exception("Invalid URL - author's id can not be null");
            }

            AuthorViewModel viewModel = this.authorService.GetAuthor(id);
            if (viewModel == null)
            {
                throw new Exception($"Invalid URL - there is no author with id {id}");
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
                throw new Exception($"Invalid URL - there is no author with name {authorName}");
            }

            return View(viewModel);
        }
    }
}
