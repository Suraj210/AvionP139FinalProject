﻿namespace Avion.Areas.Admin.ViewModels.Testimonial
{
    public class TestimonialVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool IsMain { get; set; }
        public DateTime CreateTime { get; set; }

        public bool SoftDeleted { get; set; }
    }
}
