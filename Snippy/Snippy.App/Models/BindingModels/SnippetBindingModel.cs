
namespace Snippy.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class SnippetBindingModel
    {
        public int Id { get; set; }

       // [AllowHtml]
       // [Required]
        public string Title { get; set; }

       // [AllowHtml]
       // [Required]
        public string Description { get; set; }

      //  [AllowHtml]
      //  [Required]
        public string Code { get; set; }

        //public int LanguageId { get; set; }

        //public string Labels { get; set; }
    }
}