using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.core.Models
{
    public class CourseDetailsModel:CourseModel
    {
        public List<SessionDetailModel> SessionsDetails { get; set; } = new List<SessionDetailModel>();
        public List<UserReviewModel> UserReviews { get; set; } = new List<UserReviewModel>();
    }
}
