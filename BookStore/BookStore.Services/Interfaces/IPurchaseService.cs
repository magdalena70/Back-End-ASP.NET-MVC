using System.Collections.Generic;
using BookStore.Models.BindingModels.Purchase;
using BookStore.Models.ViewModels.Purchase;

namespace BookStore.Services.Interfaces
{
    public interface IPurchaseService
    {
        void DeletePurchase(int id);
        void EditPurchase(EditPurchaseBindingModel bindingModel);
        IEnumerable<AllPurchasesViewModel> GetAll();
        DeletePurchaseViewModel GetDeletePurchaseViewModel(int? id);
        PurchaseDetailsViewModel GetDetails(int? id);
        EditPurchaseViewModel GetEditPurchaseViewModel(int? id);
    }
}