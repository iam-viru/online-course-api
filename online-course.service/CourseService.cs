using online_course.core.Models;
using online_course.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.service
{
    // new way to implement the dependency injection in .NET 6 and above
    public class CourseService(ICourseRepository courseRepository) : ICourseService
    {
        private readonly ICourseRepository _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        public Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null)
        {
            return _courseRepository.GetAllCoursesAsync(categoryId);
        }

        public async Task<CourseDetailsModel?> GetCourseDetailAsync(int courseId)
        {
            var courseDetails = await _courseRepository.GetCourseDetailAsync(courseId);
            return courseDetails;
        }
    }
}
