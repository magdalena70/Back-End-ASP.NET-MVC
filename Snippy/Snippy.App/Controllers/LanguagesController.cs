
namespace Snippy.App.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Snippy.Data.UnitOfWork;
    using Snippy.App.Models.ViewModels;
    using AutoMapper;
    using System.Collections;
    using System.Collections.Generic;
    using System;
using Snippy.Models;

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

        public ActionResult CreateLanguage(ProgrammingLanguage languageModel)
        {
            return this.View();
        }
    }
}