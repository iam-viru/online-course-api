using online_course.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.Data
{
    //public interface 
    //{
    //    CourseCategory? GetByID(int id);
    //    List<CourseCategory> GetCourseCategories();
    //}

    public interface ICourseCategoryRepository
    {
        //Asynchronous methods for better performance in I/O-bound operations
        //Asynchronous methods will always return a Task<T> or Task
        Task<CourseCategory?> GetByIdAsync(int id);
        Task<List<CourseCategory>> GetCourseCategoriesAsync();
        // Synchronous methods for backward compatibility, if needed 
    }
}
