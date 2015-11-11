using AutoMapper;
using Snippy.App.Models.ViewModels;
using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippy.App.App_Start
{
    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Snippet, SnippetViewModel>();

            Mapper.CreateMap<Snippet, SnippetsByLabelViewModel>()
                .ForMember(vm => vm.Author, options => options.MapFrom(s => s.Author.UserName))
                .ForMember(vm => vm.Language, options => options.MapFrom(s => s.Language.Name));

            Mapper.CreateMap<Snippet, SnippetsByLanguageViewModel>()
                .ForMember(vm => vm.Author, options => options.MapFrom(s => s.Author.UserName));

            Mapper.CreateMap<Snippet, UserSnippetViewModel>()
                .ForMember(vm => vm.Language, options => options.MapFrom(s => s.Language.Name));

            Mapper.CreateMap<Snippet, SnippetDetailsViewModel>()
                .ForMember(vm => vm.Author, options => options.MapFrom(s => s.Author.UserName))
                .ForMember(vm => vm.Language, options => options.MapFrom(s => s.Language.Name));

            Mapper.CreateMap<Comment, CommentViewModel>()
                .ForMember(vm => vm.Author, options => options.MapFrom(c => c.Author.UserName))
                .ForMember(vm => vm.SnippetTitle, options => options.MapFrom(c => c.Snippet.Title));

            Mapper.CreateMap<Label, LabelViewModel>()
                .ForMember(vm => vm.SnippetCount, options => options.MapFrom(l => l.Snippets.Count));

            Mapper.CreateMap<Label, LabelSnippetViewModel>()
                .ForMember(vm => vm.SnippetCount, options => options.MapFrom(l => l.Snippets.Count));

            Mapper.CreateMap<ProgrammingLanguage, LanguageSnippetViewModel>()
               .ForMember(vm => vm.SnippetCount, options => options.MapFrom(pl => pl.Snippets.Count));

            Mapper.CreateMap<User, UserViewModel>()
                .ForMember(vm => vm.SnippetCount, options => options.MapFrom(u => u.Snippets.Count));

            Mapper.CreateMap<User, UserDetailsViewModel>()
                .ForMember(vm => vm.SnippetCount, options => options.MapFrom(u => u.Snippets.Count));
        }
    }
}