namespace Avion.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public bool SoftDeleted { get; set; } = false;

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
