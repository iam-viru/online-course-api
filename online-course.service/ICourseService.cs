using online_course.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.service
{
    public interface ICourseService
    {
        Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null);
        Task<CourseDetailsModel?> GetCourseDetailAsync(int courseId);

    }
}
