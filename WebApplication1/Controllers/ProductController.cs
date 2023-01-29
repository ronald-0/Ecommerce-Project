using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interface;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _proRepo;

        public ProductController(IProduct proRepo)
        {
            _proRepo = proRepo;
        }
        // GET api/<ProductController>
        /// <summary>
        /// This is to get the list of all products
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var result = _proRepo.GetAllProducts();

            return Ok(
                new APIResponse
                {
                    data = result.products,
                    message = result.message,
                    statusCode = 200
                });
        }

        // GET api/<ProductController>
        /// <summary>
        /// This is to get a product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _proRepo.GetProductById(id);
            if (result.successful == false)
            {
                return NotFound(new APIResponse
                {
                    data = result.product,
                    message = result.message,
                    statusCode = 404
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.product,
                    message = result.message,
                    statusCode = 200
                });
        }

        // POST api/<ProductController>
        /// <summary>
        /// This is to create a product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public IActionResult Post([FromForm] CreateProductViewModel model)
        {
            var result = _proRepo.AddProduct(model);
            if (result.successful == false)
            {
                return BadRequest(new APIResponse
                {
                    data = result.product,
                    message = result.message,
                    statusCode = 400
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.product,
                    message = result.message,
                    statusCode = 200
                });
        }

        // PUT api/<ProductController>/5
        /// <summary>
        /// This is used to update a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] CreateProductViewModel model)
        {
            var result = _proRepo.UpdateProduct(model, id);
            if (result.successful == false)
            {
                return BadRequest(new APIResponse
                {
                    data = result.product,
                    message = result.message,
                    statusCode = 400
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.product,
                    message = result.message,
                    statusCode = 200
                });
        }

        // DELETE api/<ProductController>/5
        /// <summary>
        /// This is used to delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _proRepo.DeleteProductById(id);
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

        // GET api/<ProductController>
        /// <summary>
        /// This is to get all products within a specific date
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProductsWithinDates")]
        public IActionResult Get(string startDate, string endDate)
        {
            var fdate = DateTime.TryParse(startDate, out var date);
            var ldate = DateTime.TryParse(endDate, out var date1);
            if (fdate == false || ldate == false)
            {
                return BadRequest(new APIResponse
                {
                    data = null,
                    message = "wrong date format",
                    statusCode = 400
                });
            }
            var result = _proRepo.GetAllProductsInDateRange(date, date1);

            return Ok(
                new APIResponse
                {
                    data = result.products,
                    message = result.message,
                    statusCode = 200
                });
        }
    }
}
