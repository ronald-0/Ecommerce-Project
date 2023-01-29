using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interface;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAuthentication _cusRepo;

        public CustomerController(IAuthentication cusRepo)
        {
            _cusRepo = cusRepo;
        }
        // GET api/<CustomerController>
        /// <summary>
        /// This is to get the list of all customer
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var result = _cusRepo.GetAllCustomers();

            return Ok(
                new APIResponse
                {
                    data = result.customers,
                    message = result.message,
                    statusCode = 200
                });
        }

        // GET api/<CustomerController>
        /// <summary>
        /// This is to get a customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = _cusRepo.GetCustomerById(id);
            if (result.successful == false)
            {
                return NotFound(new APIResponse
                {
                    data = result.customer,
                    message = result.message,
                    statusCode = 404
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.customer,
                    message = result.message,
                    statusCode = 200
                });
        }

        // POST api/<CustomerController>
        /// <summary>
        /// This is to create a customer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Post([FromForm] CreateCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _cusRepo.AddCustomer(model);
                if (result.successful == false)
                {
                    return BadRequest(new APIResponse
                    {
                        data = result.customer,
                        message = result.message,
                        statusCode = 400
                    });
                }
                return Ok(
                    new APIResponse
                    {
                        data = result.customer,
                        message = result.message,
                        statusCode = 200
                    }); 
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<CustomerController>/5
        /// <summary>
        /// This is used to update a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromForm] CreateCustomerViewModel model)
        {
            var result = _cusRepo.UpdateCustomer(model, id);
            if (result.successful == false)
            {
                return BadRequest(new APIResponse
                {
                    data = result.customer,
                    message = result.message,
                    statusCode = 400
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.customer,
                    message = result.message,
                    statusCode = 200
                });
        }

        // DELETE api/<CustomerController>/5
        /// <summary>
        /// This is used to delete a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = _cusRepo.DeleteCustomerById(id);
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

        // GET api/<CustomerController>
        /// <summary>
        /// This is to get all categories within a specific date
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCustomersBornWithinDates")]
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
            var result = _cusRepo.GetAllCustomersBornInDateRange(date, date1);

            return Ok(
                new APIResponse
                {
                    data = result.customers,
                    message = result.message,
                    statusCode = 200
                });
        }

        // POST api/<CustomerController>
        /// <summary>
        /// This is to login.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Post([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _cusRepo.Login(model.UserName,model.Password);
                if (result.succesful == false)
                {
                    return BadRequest(new APIResponse
                    {
                        data = result.succesful,
                        message = result.message,
                        statusCode = 400
                    });
                }
                return Ok(
                    new APIResponse
                    {
                        data = result.succesful,
                        message = result.message,
                        statusCode = 200
                    });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
