namespace Avion.Models
{
    public class Wishlist:BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<WishlistProduct> WishlistProducts { get; set; }
    }
}
