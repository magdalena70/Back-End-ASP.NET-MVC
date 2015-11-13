
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
    using System.Net;
    using System.Web;
    using Snippy.App.Models.BindingModels;

    [Authorize]
    public class LanguagesController : BaseController
    {
        public LanguagesController(ISnippyData data)
            :base(data)
        {
        }

        public ActionResult SnippetsByLanguage(int id, int page = 1, int count = 3)
        {
            var language = this.Data.ProgrammingLanguages.Find(id);
            int snippetsCount = language.Snippets.Count();
            var snippets = language.Snippets
                .OrderByDescending(s => s.CreationTime)
                .ThenByDescending(s => s.Id)
                .Skip((page - 1) * count)
                .Take(count);
            this.ViewBag.TotalPages = (snippetsCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;
            var model = new LanguagePageViewModel()
            {
                Language = Mapper.Map<LanguageSnippetViewModel>(language),
                Snippets = Mapper.Map<IEnumerable<SnippetsByLanguageViewModel>>(snippets)
            };

            return this.View(model);
        }

        public ActionResult SearchByLanguage(string searchString)
        {
            var languages = from s in this.Data.ProgrammingLanguages.All()
                           select s;
            if (languages == null)
            {
                return this.HttpNotFound("No languages");
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                languages = languages.Where(s => s.Name.Contains(searchString));
            }
            else
            {
                languages = languages.OrderBy(l => l.Name).Take(5);
            }

            return this.View(languages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLanguage(ProgrammingLanguage languageModel)
        {
            if (languageModel != null && this.ModelState.IsValid)
            {
                var language = new ProgrammingLanguage()
                {
                    Name = languageModel.Name
                };

                this.Data.ProgrammingLanguages.Add(language);
                this.Data.SaveChanges();

                var languageDb = this.Data.ProgrammingLanguages.All()
                    .FirstOrDefault(c => c.Id == language.Id);
                var model = Mapper.Map<ProgrammingLanguage, ProgrammingLanguageViewModel>(languageDb);

                this.TempData["Message"] = "You added new proramming language successfully.";
                return this.PartialView("DisplayTemplates/ProgrammingLanguageViewModel", model);
            }

            return this.Json("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddSnippetInCurrentLanguage(int id, Snippet snippetModel)
        {
            if (snippetModel != null /*&& !this.ModelState.IsValid*/)
            {

                var snippet = new Snippet()
                {
                    Title = HttpUtility.HtmlEncode(snippetModel.Title),
                    Description = HttpUtility.HtmlEncode(snippetModel.Description),
                    Code = HttpUtility.HtmlEncode(snippetModel.Code),
                    Language = this.Data.ProgrammingLanguages.All().FirstOrDefault(l => l.Id == id),
                    CreationTime = DateTime.Now,
                    Author = this.Data.Users.All().FirstOrDefault(u => u.UserName == this.UserProfile.UserName)
                };

                this.Data.Snippets.Add(snippet);
                this.Data.SaveChanges();

                var snippetDb = this.Data.Snippets.All()
                    .FirstOrDefault(s => s.Id == snippet.Id);

                if(this.Data.Labels.All().Any(l => l.Text == snippetModel.LabelText)){
                    snippet.Labels = new List<Label>()
                    {
                        this.Data.Labels.All().FirstOrDefault(l => l.Text == snippetModel.LabelText)
                    };
                }else{
                    this.Data.Labels.Add(new Label{
                                Text = HttpUtility.HtmlEncode(snippetModel.LabelText),
                                Snippets = new List<Snippet>(){ snippetDb }
                            }); 
                }

                this.Data.SaveChanges();

                var model = Mapper.Map<Snippet, SnippetsByLanguageViewModel>(snippetDb);

                this.TempData["Message"] = "You added new snippet " + HttpUtility.HtmlEncode(snippetDb.Title) + " successfully.";
                return this.PartialView("DisplayTemplates/SnippetsByLanguageViewModel", model);
            }

            return this.Json("Error");
        }
    }
}