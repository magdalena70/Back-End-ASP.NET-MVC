
namespace SoftUni.Blog.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitOfWork;
    using SoftUni.Blog.Models;
    using Models.ViewModels;

    public class PostsController : BaseController
    {
        public PostsController(ISoftUniBlogData data)
            : base(data)
        {
        }

        public ActionResult Details(int id)
        {
            var post = this.Data.Posts.Find(id);
            if (post == null)
            {
                return this.HttpNotFound();
            }

            var postModel = Mapper.Map<Post, PostFullViewModel>(post);
            return View(postModel);
        }
    }
}