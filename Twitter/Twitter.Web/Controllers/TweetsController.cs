
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

    public class TweetsController : BaseController
    {
        public TweetsController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {

            return this.View();
        }
    }
}