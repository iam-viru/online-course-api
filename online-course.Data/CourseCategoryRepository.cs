using Microsoft.EntityFrameworkCore;
using online_course.core.Data;
using online_course.core.Entities;
using online_course.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_course.Data
{

    #region All methods are implemented as synchronous methods

    //this is primary constructor syntax in C# 12.0, and .net 8.0
    //public class CourseCategoryRepository(OnlineCourseDbContext dbContext) : ICourseCategoryRepository
    //{
    //    private readonly OnlineCourseDbContext _dbContext = dbContext;

    //    //this menas that the class implements the ICourseCategoryRepository interface
    //    //and it will return null if the course category is not found in the datbase
    //    public CourseCategory? GetByID(int id)
    //    {
    //        var courseCategory = _dbContext.CourseCategories.Find(id);
    //        if (courseCategory == null)
    //        {
    //            throw new KeyNotFoundException($"CourseCategory with ID {id} not found.");
    //        }
    //        return courseCategory;
    //    }

    //    public List<CourseCategory> GetCourseCategories()
    //    {
    //        var courseCategories = _dbContext.CourseCategories.ToList();
    //        if (courseCategories == null || !courseCategories.Any())
    //        {
    //            throw new InvalidOperationException("No course categories found.");
    //        }
    //        return courseCategories;
    //    }
    //}

    #endregion All methods are implemented as synchronous methods
}
#region All methods are implemented as asynchronous methods

//This is primary constructor syntax starting from C# 12.0, and .net 8.0
public class CourseCategoryRepository(OnlineCourseDbContext dbContext) : ICourseCategoryRepository
{
    private readonly OnlineCourseDbContext _dbContext=dbContext;

    #region This is olde syntax for C# 11.0 and .NET 7.0

    //public CourseCategoryRepository(OnlineCourseDbContext dbContext)
    //{
    //    _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    //}
    #endregion


    // As long as we dont need immediate results, we dont need to use async and await keywords.
    // the moment we use async and await keywords, the data will be fetched immediately and the calls will be suspednded
    //there itself.
    public Task<CourseCategory?> GetByIdAsync(int id)
    {
        var courseCategory = _dbContext.CourseCategories.FindAsync(id).AsTask();
        return courseCategory is not null ? courseCategory : Task.FromResult<CourseCategory?>(null);
    }       

    public Task<List<CourseCategory>> GetCourseCategoriesAsync()
    {
        var data=_dbContext.CourseCategories.ToListAsync();
        return data;
    }
}
#endregion
