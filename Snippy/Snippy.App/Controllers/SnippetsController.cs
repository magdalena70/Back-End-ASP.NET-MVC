
namespace Snippy.App.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Snippy.Data.UnitOfWork;
    using Snippy.App.Models.ViewModels;
    using Snippy.Models;
    using AutoMapper;
    using System.Web;

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
                searchString = HttpUtility.HtmlEncode(searchString);
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