
namespace Snippy.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Snippy.Data.UnitOfWork;
    using Snippy.App.Models.ViewModels;
    using AutoMapper;
    using System.Collections;
    using System.Collections.Generic;
    using System;

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

        public ActionResult SnippetDetails(int id)
        {
            var snippet = this.Data.Snippets.Find(id);
            if (snippet == null)
            {
                return this.HttpNotFound("Snippet is not found.");
            }
            var model = Mapper.Map<SnippetDetailsViewModel>(snippet);

            return this.View(model);
        }

        public ActionResult SearchSnippet(string searchString)
        {
            var snippets = from s in this.Data.Snippets.All().AsQueryable()
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                snippets = snippets.Where(s => s.Title.Contains(searchString));
            }

            return this.View(snippets);
        }
    }
}