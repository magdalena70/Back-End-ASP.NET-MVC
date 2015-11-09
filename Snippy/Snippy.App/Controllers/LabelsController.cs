
namespace Snippy.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Snippy.Data.UnitOfWork;
    using Snippy.App.Models.ViewModels;
    using AutoMapper;
    using System.Collections;
    using System.Collections.Generic;

    public class LabelsController : BaseController
    {
        public LabelsController(ISnippyData data)
            :base(data)
        {
        }

        // GET: Labels
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
    }
}