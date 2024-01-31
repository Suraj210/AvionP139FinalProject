namespace Avion.Models
{
    public class WishlistProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WishlistId { get; set; }
        public Product Product { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
