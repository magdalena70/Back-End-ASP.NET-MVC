
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
using Snippy.Models;

    public class LabelsController : BaseController
    {
        public LabelsController(ISnippyData data)
            :base(data)
        {
        }

        public ActionResult SnippetsByLabel(int id, int page = 1, int count = 3)
        {
            var label = this.Data.Labels.Find(id);
            int snippetsCount = label.Snippets.Count();
            var snippets = label.Snippets
                .OrderByDescending(s => s.CreationTime)
                .ThenByDescending(s => s.Id)
                .Skip((page - 1) * count)
                .Take(count);
            this.ViewBag.TotalPages = (snippetsCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;
            var model = new LabelPageViewModel()
            {
                Label = Mapper.Map<LabelSnippetViewModel>(label),
                Snippets = Mapper.Map<IEnumerable<SnippetsByLabelViewModel>>(snippets)
            };

            return View(model);
        }

        public ActionResult SearchByLabel(string searchString)
        {
            var labels = from l in this.Data.Labels.All()
                           select l;
            if (labels == null)
            {
                return this.HttpNotFound("No labels");
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                labels = labels.Where(l => l.Text.Contains(searchString));
            }
            else
            {
                labels = labels.OrderBy(l => l.Text).Take(5);
            }

            return this.View(labels);
        }

        public ActionResult SelectLabel()
        {
            var allLabels = this.Data.Labels.All();
            List<SelectListItem> items = new List<SelectListItem>();
            var count = 0;
            foreach (var label in allLabels)
            {
                items.Add(new SelectListItem { Text = label.Text, Value = label.Id.ToString() });
                count++;
            }

            ViewBag.SnippetLabels = items;
            return this.View();
        }

        [HttpPost]
        public ActionResult SelectLabel(int id, string SnippetLabels)
        {
            var snippet = this.Data.Snippets.Find(id);
            snippet.Labels.Add(this.Data.Labels.Find(int.Parse(SnippetLabels)));
            this.Data.SaveChanges();

            //var selectedLabel = this.Data.Labels.Find(int.Parse(SnippetLabels));
            return this.RedirectToAction("SnippetDetails", "Snippets", new { snippetId = id });
        }
    }
}