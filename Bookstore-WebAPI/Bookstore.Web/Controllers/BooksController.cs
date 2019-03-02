using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Bookstore.Data;
using Bookstore.Models.EntityModels;
using Microsoft.AspNet.Identity;
using Bookstore.Services;
using Bookstore.Models.ViewModels.Books;
using Bookstore.Models.BindingModels.Books;
using System.Collections.Generic;

namespace Bookstore.Web.Controllers
{
    [AllowAnonymous]
    public class BooksController : ApiController
    {
        private BookService bookService;

        public BooksController()
        {
            this.bookService = new BookService();
        }

        /// <summary>
        /// Get all books from database
        /// </summary>
        /// <returns>AllBooksViewModel</returns>
        // GET: api/Books
        public IEnumerable<AllBooksViewModel> GetBooks()
        {
            IEnumerable<AllBooksViewModel> books = bookService.GetAllBooks();   
            return books;
        }

        /// <summary>
        /// Get book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns book's details</returns>
        // GET: api/Books/5
        [ResponseType(typeof(BookDetailsViewModel))]
        public IHttpActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            string currUserId = User.Identity.GetUserId();
            BookDetailsViewModel viewModel = bookService.GetDetails(id, currUserId);
            if (viewModel == null)
            {
                return NotFound();
            }

            return Ok(viewModel);
        }


        /// <summary>
        /// Edit book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bindingModel"></param>
        /// <returns>content of edited book</returns>
        // PUT: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult PutBook(int id, EditBookBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bindingModel.Id)
            {
                return BadRequest();
            }

            try
            {
                this.bookService.EditBook(bindingModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return StatusCode(HttpStatusCode.NoContent);
            Book book = this.bookService.GetCurrentBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        /// <summary>
        /// Add new book
        /// </summary>
        /// <param name="bindingModel"></param>
        /// <returns></returns>
        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(AddBookBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.bookService.AddBook(bindingModel);
            Book newBook = this.bookService.GetNewBooks(bindingModel.Title, bindingModel.ISBN);

            return CreatedAtRoute("DefaultApi", new { id = newBook.Id }, newBook);
        }

        /// <summary>
        /// Delete book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book book = this.bookService.GetCurrentBook((int)id);
            if (book == null)
            {
                return NotFound();
            }

            this.bookService.DeleteBook(book);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool BookExists(int id)
        {
            return this.bookService.GetCurrentBook(id) != null;
        }
    }
}