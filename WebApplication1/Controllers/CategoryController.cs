using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;
using WebApplication1.Interface;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _catRepo;

        public CategoryController(ICategory catRepo)
        {
            _catRepo = catRepo;
        }
        // GET api/<CategoryController>
        /// <summary>
        /// This is to get the list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var result = _catRepo.GetAllCategories();

            return Ok(
                new APIResponse
                {
                    data= result.categories, 
                    message = result.message, 
                    statusCode = 200
                });
        }

        // GET api/<CategoryController>
        /// <summary>
        /// This is to get a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _catRepo.GetCategoryById(id);
            if(result.successful == false)
            {
                return NotFound(new APIResponse
                {
                    data = result.category,
                    message = result.message,
                    statusCode = 404
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.category,
                    message = result.message,
                    statusCode = 200
                });
        }

        // POST api/<CategoryController>
        /// <summary>
        /// This is to create a category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public IActionResult Post([FromForm] CreateCategoryViewModel model)
        {
            var result = _catRepo.AddCategory(model);
            if (result.successful == false)
            {
                return BadRequest(new APIResponse
                {
                    data = result.category,
                    message = result.message,
                    statusCode = 400
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.category,
                    message = result.message,
                    statusCode = 200
                });
        }

        // PUT api/<CategoryController>/5
        /// <summary>
        /// This is used to update a category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] CreateCategoryViewModel model)
        {
            var result = _catRepo.UpdateCategory(model, id);
            if (result.successful == false)
            {
                return BadRequest(new APIResponse
                {
                    data = result.category,
                    message = result.message,
                    statusCode = 400
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.category,
                    message = result.message,
                    statusCode = 200
                });
        }

        // DELETE api/<CategoryController>/5
        /// <summary>
        /// This is used to delete a category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _catRepo.DeleteCategoryById(id);
            if (result.successful == false)
            {
                return NotFound(new APIResponse
                {
                    data = null,
                    message = result.message,
                    statusCode = 404
                });
            }
            return Ok(
                new APIResponse
                {
                    data = null,
                    message = result.message,
                    statusCode = 200
                });
        }

        // GET api/<CategoryController>
        /// <summary>
        /// This is to get all categories within a specific date
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCategoriesWithinDates")]
        public IActionResult Get(string startDate, string endDate)
        {
            var fdate = DateTime.TryParse(startDate, out var date);
            var ldate = DateTime.TryParse(endDate, out var date1);
            if(fdate == false || ldate == false) 
            {
                return BadRequest(new APIResponse
                {
                    data = null,
                    message = "wrong date format",
                    statusCode = 400
                });
            }
            var result = _catRepo.GetAllCategoriesInDateRange(date, date1);

            return Ok(
                new APIResponse
                {
                    data = result.categories,
                    message = result.message,
                    statusCode = 200
                });
        }
    }
}
