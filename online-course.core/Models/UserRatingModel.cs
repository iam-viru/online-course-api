using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.core.Models
{
    public class UserRatingModel
    {
        public int CourseId { get; set; }

        public decimal AverageRating { get; set; }

        public int TotalRating { get; set; }

    }
}
