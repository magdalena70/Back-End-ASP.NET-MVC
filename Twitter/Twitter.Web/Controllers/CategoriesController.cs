
namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Twitter.Data;
    using Twitter.Web.ViewModels;
    using Twitter.Models;

    public class CategoriesController : BaseController
    {
        public CategoriesController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult Index(string name)
        {
            var categories = this.Data.Categories.All();
            var names = categories.Select(c => c.Name).ToList();

            return this.Content(string.Format("{0}", string.Join(", ", names)));
        }
    }
}