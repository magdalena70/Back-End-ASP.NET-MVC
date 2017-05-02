using BookStore.Models.ViewModels.Home;

namespace BookStore.Services.Interfaces
{
    public interface IHomeService
    {
        HomePageViewModel GetHomePageViewModel();
    }
}