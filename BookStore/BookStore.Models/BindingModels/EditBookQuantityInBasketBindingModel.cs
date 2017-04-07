namespace BookStore.Models.BindingModels
{
    public class EditBookQuantityInBasketBindingModel
    {
        public int BookId { get; set; }

        public int Count { get; set; }

        public int NewCount { get; set; }
    }
}
