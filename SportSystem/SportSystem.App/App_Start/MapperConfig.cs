
namespace SportSystem.App.App_Start
{
    using AutoMapper;
    using SportSystem.App.Models.ViewModels;
    using SportSystem.Models;

    public static class MapperConfig
    {

        public static void RegisterMappings()
        {
            Mapper.CreateMap<Match, ConciseMatchViewModel>()
                .ForMember(vm => vm.HomeTeamName, options => options.MapFrom(m => m.HomeTeam.Name))
                .ForMember(vm => vm.AwayTeamName, options => options.MapFrom(m => m.AwayTeam.Name));

            Mapper.CreateMap<Team, ConciseTeamViewModel>()
                .ForMember(vm => vm.VotesCount, options => options.MapFrom(t => t.Votes.Count));
        }
    }
}