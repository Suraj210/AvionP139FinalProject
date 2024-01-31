namespace Avion.ViewModels.Basket
{
    public class BasketDetailVM
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
