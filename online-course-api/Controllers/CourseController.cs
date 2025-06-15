using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_course.core.Models;
using online_course.service;

namespace online_course_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseService courseService) : ControllerBase
    {
        private readonly ICourseService _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));



        //api/course
        [HttpGet]
        public async Task<ActionResult<List<CourseModel>>> GetAllCoursesAsync()
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching courses.");
            }
        }

        //api/course/Category/?categoryId={categoryId}

        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<List<CourseModel>>> GetCoursesByCategoryIdAsync(int categoryId)
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync(categoryId);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching courses by category.");
            }
        }

        //api/course/Detail/{courseId}

        [HttpGet("Detail/{courseId}")]
        public async Task<ActionResult<CourseDetailsModel?>> GetCourseDetailAsync(int courseId)
        {
            try
            {
                var courseDetail = await _courseService.GetCourseDetailAsync(courseId);
                if (courseDetail == null)
                {
                    return NotFound($"Course with ID {courseId} not found.");
                }
                return Ok(courseDetail);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching course details.");
            }


        }
    }
}
