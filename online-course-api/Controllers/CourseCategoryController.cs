using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_course.service;

namespace online_course_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // so here conttroller is calling service and service is calling repository. 
    public class CourseCategoryController(ICourseCategoryService courseCategoryService) : ControllerBase
    {
        private readonly ICourseCategoryService _courseCategoryService = courseCategoryService;
        
        #region Older way to inject the service using DI
        //public CourseCategoryController(ICourseCategoryService courseCategoryService )
        //{
        //        _courseCategoryService = courseCategoryService;
        //}
        #endregion


    }
}
