using online_course.core.Models;

namespace online_course.service
{
    //this is the place where we define the business logic for course categories
    public interface ICourseCategoryService
    {
        Task<CourseCategoryModel?> GetByIdAsync(int id);
        Task<List<CourseCategoryModel>> GetCourseCategoriesAsync();

    }
}
