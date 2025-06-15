using Microsoft.EntityFrameworkCore;
using online_course.core.Data;
using online_course.core.Models;

namespace online_course.Data
{

    //This is primary constructor syntax starting from C# 12.0, and .net 8.0
    public class CourseRepository(OnlineCourseDbContext onlineCourseDb) : ICourseRepository
    {
        private readonly OnlineCourseDbContext _onlineCourseDb = onlineCourseDb;

        public async Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null)
        {
            //here we have used the AsQueryable this means that it will not execute the query until we await or use 
            //sync method like ToList 
            var courses = _onlineCourseDb.Courses
                 .Include(c => c.Category)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                // here we are filtering the courses based on the category id and this will filter the coused based on the category id in the server side and not in the client side
                // so this will be replaced with the where clause in the sql query
                courses = courses.Where(c => c.CategoryId == categoryId.Value);

            }
            var courseList = await courses
                .Select(c => new CourseModel()
                {
                    CourseId = c.CourseId,
                    Tittle = c.Title,
                    Description = c.Description,
                    CourseType = c.CourseType,
                    Price = c.Price,
                    SeatsAvailable = c.SeatsAvailable.GetValueOrDefault(),
                    CategoryId = c.CategoryId,
                    InstructorID = c.InstructorId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Category = new CourseCategoryModel()
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.CategoryName,
                        Description = c.Category.Description
                    },
                    UserRating = new UserRatingModel()
                    {
                        CourseId = c.CourseId,
                        AverageRating = c.Reviews.Any() ? Convert.ToDecimal(c.Reviews.Average(r => r.Rating)) : 0,
                        TotalRating = c.Reviews.Count()
                    }
                }).ToListAsync();
            return courseList;
        }

        /// <summary>
        /// This method will return all the courses based on the category id and also include all the related records like category, reviews, session details, and user rating.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<CourseDetailsModel> GetCourseDetailAsync(int categoryId)
        {
            var courses = await _onlineCourseDb.Courses
                .Include(c => c.Category)
                .Include(c => c.Reviews)
                .Include(c => c.SessionDetails)
                .Where(c => c.CategoryId == categoryId)
                .Select(c => new CourseDetailsModel()
                {
                    CourseId = c.CourseId,
                    Tittle = c.Title,
                    Description = c.Description,
                    CourseType = c.CourseType,
                    Price = c.Price,
                    SeatsAvailable = c.SeatsAvailable.GetValueOrDefault(),
                    CategoryId = c.CategoryId,
                    InstructorID = c.InstructorId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Category = new CourseCategoryModel()
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.CategoryName,
                        Description = c.Category.Description
                    },
                    UserReviews = c.Reviews.Select(r => new UserReviewModel()
                    {
                        CourseId = r.CourseId,
                        ReviewId = r.ReviewId,
                        UserId = r.UserId,
                        UserName = r.User.DisplayName,
                        Rating = r.Rating,
                        Comments = r.Comments,
                        ReviewDate = r.ReviewDate
                    }).OrderBy(o => o.Rating).Take(10).ToList(),
                    SessionsDetails = c.SessionDetails.Select(s => new SessionDetailModel()
                    {
                        SessionId = s.SessionId,
                        CourseId = s.CourseId,
                        Title = s.Title,
                        Description = s.Description,
                        VideoUrl = s.VideoUrl,
                        VideoOrder = s.VideoOrder
                    }).OrderBy(o => o.VideoOrder).ToList(),
                    UserRating = new UserRatingModel()
                    {
                        CourseId = c.CourseId,
                        AverageRating = c.Reviews.Any() ? Convert.ToDecimal(c.Reviews.Average(r => r.Rating)) : 0,
                        TotalRating = c.Reviews.Count()
                    }
                }).FirstOrDefaultAsync();

            if (courses == null)
            {
                throw new InvalidOperationException($"No course found for category ID {categoryId}");
            }   

            return courses;
        }
    }
}
