using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels.User
{
    public class EditUserMoneySpentBalanceBindingModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal MoneySpentBalance { get; set; }
    }
}
