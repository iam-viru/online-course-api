using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.core.Models
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public string Tittle { get; set; }
        public string? Thumbnail { get; set; }
        public string Description { get; set; }
        public string CourseType { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }
        public int CategoryId { get; set; } // Foreign key to CourseCategory
        public int InstructorID { get; set; } // Foreign key to CourseCategory
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property for the related CourseCategory
        public CourseCategoryModel Category { get; set; }
        public UserRatingModel UserRating { get; set; } // Navigation property for UserRating
    }
}
