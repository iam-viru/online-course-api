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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        { 
        var category=await _courseCategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();// means return 404 code
            }
            return Ok(category); // means it will return the 200 http code
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var categoriesList = await _courseCategoryService.GetCourseCategoriesAsync();
            if (categoriesList == null)
            { 
            return BadRequest();
            }
            return Ok(categoriesList);
        }
    }
}
