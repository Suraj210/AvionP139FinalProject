using Microsoft.Build.Evaluation;

namespace Avion.Models
{
    public class About : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }
}
