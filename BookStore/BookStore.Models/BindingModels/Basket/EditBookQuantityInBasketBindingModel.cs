namespace BookStore.Models.BindingModels.Basket
{
    public class EditBookQuantityInBasketBindingModel
    {
        public int BookId { get; set; }

        public int Count { get; set; }

        public int NewCount { get; set; }
    }
}
