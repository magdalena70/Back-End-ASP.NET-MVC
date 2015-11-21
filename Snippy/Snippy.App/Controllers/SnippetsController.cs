
namespace Snippy.App.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity;
    using Snippy.Data.UnitOfWork;
    using Snippy.App.Models.ViewModels;
    using Snippy.Models;
    using AutoMapper;
    using System.Net;
    using Snippy.App.Models.BindingModels;

    [Authorize]
    public class SnippetsController : BaseController
    {
        public SnippetsController(ISnippyData data)
            :base(data)
        {
        }

        // GET: Snippets
        public ActionResult Index(int page = 1, int count = 3)
        {
            var snippets = this.Data.Snippets.All();
            int snippetsCount = snippets.Count();
            snippets = snippets
                .OrderByDescending(s => s.CreationTime)
                .ThenByDescending(s => s.Id)
                .Skip((page - 1) * count)
                .Take(count);
            this.ViewBag.TotalPages = (snippetsCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;
            var model = Mapper.Map<IEnumerable<SnippetViewModel>>(snippets);

            return this.View(model);

        }

        public ActionResult SnippetDetails(int snippetId)
        {
            var snippet = this.Data.Snippets.Find(snippetId);
            if (snippet == null)
            {
                return this.HttpNotFound("Snippet is not found.");
            }

            var model = Mapper.Map<SnippetDetailsViewModel>(snippet);

            return this.View(model);
        }

        public ActionResult SnippetDetailsToDelete(int snippetId)
        {
            var snippet = this.Data.Snippets.Find(snippetId);
            if (snippet == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<SnippetDetailsToDeleteViewModel>(snippet);

            return this.View(model);
        }

        public ActionResult DeleteSnippet(int snippetId)
        {
            var snippet = this.Data.Snippets.Find(snippetId);
            if (snippet == null)
            {
                return this.HttpNotFound();
            }

            if (snippet.Comments.Count() == 0)
            {
                this.Data.Snippets.Remove(snippet);
            }
            else
            {
                var snippetComments = this.Data.Comments.All()
                    .Include(c => c.Snippet)
                    .Where(c => c.Snippet.Id == snippetId)
                    .ToList();
                foreach (var comment in snippetComments)
                {
                    this.Data.Comments.Remove(comment);
                }

                this.Data.Snippets.Remove(snippet);
            }
            
            this.Data.SaveChanges();

            this.TempData["Message"] = "The Snippet was removed successfully.";
            return RedirectToAction("UserDetails", "Users", new { username = this.UserProfile.UserName });
        }

        public ActionResult SelectCurrentSnippetLabels(int snippetId)
        {
            var currentSnippet = this.Data.Snippets.Find(snippetId);
            List<SelectListItem> items = new List<SelectListItem>();
            var count = 0;
            foreach (var label in currentSnippet.Labels.ToList())
            {
                items.Add(new SelectListItem { Text = label.Text, Value = label.Id.ToString() });
                count++;
            }

            ViewBag.CurrentSnippetLabels = items;
            return this.PartialView();
        }

        //to do
       /* public ActionResult EditSnippetDetails(int snippetId)
        {
            var snippet = this.Data.Snippets.Find(snippetId);
            var model = Mapper.Map<SnippetBindingModel>(snippet);

            return this.View(model);
        }
        //to do
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditSnippetDetails(int snippetId, SnippetBindingModel snippetModel)
        {
            var snippet = this.Data.Snippets.Find(snippetId);
            snippet.Title = snippetModel.Title;
            this.Data.SaveChanges();

            return this.RedirectToAction("SnippetDetails", "Snippets", new { snippetId = snippet.Id });
        }*/

        [ValidateInput(false)]
        public ActionResult SearchSnippet(string searchString)
        {
            var snippets = from s in this.Data.Snippets.All()
                        select s;
            if (snippets == null)
            {
                return this.HttpNotFound("No snippets");
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = HttpUtility.HtmlDecode(searchString);
                snippets = snippets.Where(s => s.Title.Contains(searchString));
            }
            else
            {
                snippets = snippets.OrderBy(s => s.Id).Take(5);
            }

            return this.View(snippets);
        }
    }
}