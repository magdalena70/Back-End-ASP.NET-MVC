
namespace Snippy.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Snippy.Data.UnitOfWork;
    using Snippy.App.Models.ViewModels;
    using AutoMapper;
    using System.Collections;
    using System.Collections.Generic;

    public class HomeController : BaseController
    {
        public HomeController(ISnippyData data )
            :base(data)
        {
        }

        public ActionResult Index()
        {
            var fiveLatestSnippets = this.Data.Snippets.All()
                .OrderByDescending(s => s.CreationTime)
                .ThenByDescending(s => s.Id)
                .Take(5);

            var fiveLatestComments = this.Data.Comments.All()
                .OrderByDescending(c => c.CreationTime)
                .ThenByDescending(c => c.Id)
                .Take(5);

            var fiveBestLabels = this.Data.Labels.All()
                .OrderByDescending(l => l.Snippets.Count)
                .ThenBy(l => l.Id)
                .Take(5);

            var model = new HomePageViewModel()
            {
                FiveLatestSnippets = Mapper.Map<IEnumerable<SnippetViewModel>>(fiveLatestSnippets),
                FiveLatestComments = Mapper.Map<IEnumerable<CommentViewModel>>(fiveLatestComments),
                FiveBestLabels = Mapper.Map<IEnumerable<LabelViewModel>>(fiveBestLabels)
            };

            return this.View(model);
        }
    }
}