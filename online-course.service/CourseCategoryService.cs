using online_course.core.Models;
using online_course.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.service
{
    public class CourseCategoryService(ICourseCategoryRepository courseCategory ) : ICourseCategoryService
    { 
        private readonly ICourseCategoryRepository _courseCategoryRepository=courseCategory;
        public async Task<CourseCategoryModel?> GetByIdAsync(int id)
        {

            //when we need data, we need to use await keyword in front of the calling method
            //also whenever the is await keyword async keyword is also needed in the method signature
            var courseCategory = await _courseCategoryRepository.GetByIdAsync(id);
            return courseCategory==null?null: new CourseCategoryModel
            {
                CategoryId = courseCategory.CategoryId,
                CategoryName = courseCategory.CategoryName,
                Description = courseCategory.Description
            };
        }

        public async Task<List<CourseCategoryModel>> GetCourseCategoriesAsync()
        {
            var courseCategories = await _courseCategoryRepository.GetCourseCategoriesAsync();
            return courseCategories.Select(c => new CourseCategoryModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description
            }).ToList();
        }
    }
}
