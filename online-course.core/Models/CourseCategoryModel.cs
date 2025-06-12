using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.core.Models
{
    //this will be used to represent the course category model in the UI
    public class CourseCategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
