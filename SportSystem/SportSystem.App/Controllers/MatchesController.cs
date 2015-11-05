namespace SportSystem.App.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models.ViewModels;
    using System.Data.Entity;
    using System.Linq;

    public class MatchesController : BaseController
    {
        public MatchesController(ISportSystemData data)
            : base(data)
        {
        }

        public ActionResult Index(int page = 1, int count = 3)
        {
            var matches = this.Data.Matches.All();
            int matchesCount = matches.Count();
            matches = matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .OrderBy(m => m.DateAndTime)
                .Skip((page - 1) * count)
                .Take(count);
            this.ViewBag.TotalPages = (matchesCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;
            var model = Mapper.Map<IEnumerable<ConciseMatchViewModel>>(matches);
            return this.View(model);
        }
    }
}