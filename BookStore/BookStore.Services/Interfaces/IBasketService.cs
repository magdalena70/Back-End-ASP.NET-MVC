using BookStore.Models.EntityModels;
using BookStore.Models.ViewModels.Basket;

namespace BookStore.Services.Interfaces
{
    public interface IBasketService
    {
        void AddBookToBasket(User currUser, Book currBook);
        void BuyBooks(User currUser);
        void ClearBasket(User currUser);
        void EditBookQuantityInBasket(Book currentBook, User currUser, int currQty, int newCount);
        BasketViewModel GetBasketDetails(string ownerId);
        Book GetCurrentBook(int bookId);
        User GetCurrentUser(string authenticatedUserId);
        void RemoveAllOfThisFromBasket(Book currentBook, User currUser, int count);
        void RemoveOneOfThisFromBasket(Book currentBook, User currUser);
    }
}