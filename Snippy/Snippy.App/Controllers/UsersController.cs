using AutoMapper;
using Snippy.App.Models.ViewModels;
using Snippy.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace Snippy.App.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(ISnippyData data)
            :base(data)
        {
        }

        // GET: Users
        public ActionResult Index(int page = 1, int count = 3)
        {
            var users = this.Data.Users.All();
            int usersCount = users.Count();
            users = users
                .OrderBy(s => s.UserName)
                .ThenByDescending(s => s.Id)
                .Skip((page - 1) * count)
                .Take(count);
            this.ViewBag.TotalPages = (usersCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;
            var model = Mapper.Map<IEnumerable<UserViewModel>>(users);

            return View(model);
        }

        public ActionResult UserDetails(string username, int page = 1, int count = 3)
        {
            var user = this.Data.Users.All()
                .FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return this.HttpNotFound("Incorrect username - user is not found");
            }

            var snippets = this.Data.Snippets.All()
                .Include(s => s.Author)
                .Where(s => s.Author.UserName == username)
                .OrderByDescending(s => s.CreationTime)
                .ThenByDescending(s => s.Id)
                .Skip((page - 1) * count)
                .Take(count);

            int snippetsCount = user.Snippets.Count();
            this.ViewBag.TotalPages = (snippetsCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;
            var model = new UserPageViewModel()
            {
                UserDetails = Mapper.Map <UserDetailsViewModel>(user),
                UserSnippets = Mapper.Map<IEnumerable<UserSnippetViewModel>>(snippets)
            };

            return this.View(model);
        }

        public ActionResult SearchUser(string text)
        {
            return this.View();
        }
    }
}