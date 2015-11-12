
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

    [Authorize]
    public class CommentsController : BaseController
    {
        public CommentsController(ISnippyData data)
            :base(data)
        {
        }

        public ActionResult CommentDetails(int id)
        {
            var comment = this.Data.Comments.Find(id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<TopCommentViewModel>(comment);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Comment commentModel, int snippetId)
        {
            if (commentModel != null && this.ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    CreationTime = DateTime.Now,
                    Content = commentModel.Content,
                    Author = this.Data.Users.All().FirstOrDefault(u => u.UserName == this.UserProfile.UserName),
                    Snippet = this.Data.Snippets.Find(snippetId)
                };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var commentDb = this.Data.Comments.All()
                    .FirstOrDefault(c => c.Id == comment.Id);
                var model = Mapper.Map<Comment, CommentViewModel>(commentDb);

                return this.PartialView("DisplayTemplates/CommentViewModel", model);
            }

            return this.Json("Error");
        }

        public ActionResult EditCommentDetails(int id, Comment commentModel)
        {
            var comment = this.Data.Comments.Find(id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }

            var snippetId = comment.Snippet.Id;

            if (ModelState.IsValid)
            {
                comment.Content = commentModel.Content;
                
                this.Data.SaveChanges();

                this.TempData["Message"] = "The comment was updated successfully.";
                return RedirectToAction("SnippetDetails", "Snippets", new { snippetId = snippetId });
            }

            return this.View(comment);
        }

        public ActionResult DeleteComment(int id, int snippetId)
        {
            var comment = this.Data.Comments.Find(id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Comments.Remove(comment);
            this.Data.SaveChanges();

            this.TempData["Message"] = "The comment was removed successfully.";
            return RedirectToAction("SnippetDetails", "Snippets", new { snippetId = snippetId });
        }
    }
}