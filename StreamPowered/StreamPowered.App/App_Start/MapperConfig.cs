
namespace StreamPowered.App.App_Start
{
    using System.Linq;
    using AutoMapper;
    using StreamPowered.App.Models.ViewModels;
    using StreamPowered.Models;

    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Genre, GenreViewModel>()
               .ForMember(vm => vm.GameCount, options => options.MapFrom(g => g.Games.Count));

            Mapper.CreateMap<Game, TopFiveGamesViewModel>()
               .ForMember(vm => vm.RatingCount, options => options.MapFrom(g => g.Ratings.Count))
               .ForMember(vm => vm.ImageUrls, options => options.MapFrom(g => g.ImageUrls.Take(1)));

            Mapper.CreateMap<Game, GameDetailsViewModel>()
                .ForMember(vm => vm.Author, options => options.MapFrom(g => g.Author.UserName))
                .ForMember(vm => vm.Genre, options => options.MapFrom(g => g.Genre.Name))
                .ForMember(vm => vm.GenreId, options => options.MapFrom(g => g.Genre.Id));

            Mapper.CreateMap<Review, TopFiveReviewsViewModel>()
               .ForMember(vm => vm.Author, options => options.MapFrom(r => r.Author.UserName))
               .ForMember(vm => vm.GameTitle, options => options.MapFrom(r => r.Game.Title))
               .ForMember(vm => vm.GameId, options => options.MapFrom(r => r.Game.Id));

            Mapper.CreateMap<Review, ReviewDetailsViewModel>()
               .ForMember(vm => vm.Author, options => options.MapFrom(r => r.Author.UserName))
               .ForMember(vm => vm.AuthorId, options => options.MapFrom(r => r.Author.Id))
               .ForMember(vm => vm.GameTitle, options => options.MapFrom(r => r.Game.Title))
               .ForMember(vm => vm.GameId, options => options.MapFrom(r => r.Game.Id));
        }
    }
}