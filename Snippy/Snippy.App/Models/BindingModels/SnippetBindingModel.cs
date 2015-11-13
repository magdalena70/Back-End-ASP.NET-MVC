
namespace Snippy.App.Models.BindingModels
{
    using Snippy.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SnippetBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Code { get; set; }

        public DateTime CreationTime { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public virtual ProgrammingLanguage Language { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Label> Labels { get; set; }

        public string LabelText { get; set; }
    }
}