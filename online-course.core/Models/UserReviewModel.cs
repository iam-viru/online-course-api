using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.core.Models
{
    //this will be used to store the user reviews for the courses
    public class UserReviewModel
    {
        public int ReviewId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string? Comments { get; set; }

        public DateTime ReviewDate { get; set; }

    }
}
