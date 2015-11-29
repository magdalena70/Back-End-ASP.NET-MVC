
namespace StreamPowered.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using StreamPowered.Data.UnitOfWork;
    using System.Collections.Generic;
    using System.Linq;
using StreamPowered.Models;

    public class UsersController : BaseAdminController
    {
        public UsersController(IStreamPoweredData data)
            : base(data)
        {
        }

        public ActionResult SelectAuthor()
        {
            var allUsers = this.Data.Users.All();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var user in allUsers)
            {
                items.Add(new SelectListItem { Text = user.UserName, Value = user.Id });
            }

            ViewBag.SelectAuthor = items;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectAuthor(int id, string SelectAuthor)
        {
            var game = this.Data.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            game.Author = this.Data.Users.All()
                .FirstOrDefault(u => u.Id == SelectAuthor);
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", "Games", new { id = game.Id });
        }
    }
}