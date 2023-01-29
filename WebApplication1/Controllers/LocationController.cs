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
    public class LocationController : ControllerBase
    {
        private readonly ILocation _locRepo;

        public LocationController(ILocation locRepo)
        {
            _locRepo = locRepo;
        }
        // GET: api/<LocationController>
        /// <summary>
        /// This is to get the list of all locations
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var result = _locRepo.GetAllLocations();

            return Ok(
                new APIResponse
                {
                    data = result.locations,
                    message = result.message,
                    statusCode = 200
                });
        }

        // GET api/<LocationController>/5
        /// <summary>
        /// This is to get a location by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _locRepo.GetLocationById(id);
            if (result.successful == false)
            {
                return NotFound(new APIResponse
                {
                    data = result.location,
                    message = result.message,
                    statusCode = 404
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.location,
                    message = result.message,
                    statusCode = 200
                });
        }

        // POST api/<LocationController>
        /// <summary>
        /// This is to create a location.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public IActionResult Post([FromForm] CreateLocationViewModel model)
        {
            var result = _locRepo.AddLocation(model);
            if (result.successful == false)
            {
                return BadRequest(new APIResponse
                {
                    data = result.location,
                    message = result.message,
                    statusCode = 400
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.location,
                    message = result.message,
                    statusCode = 200
                });
        }

        // PUT api/<LocationController>/5
        /// <summary>
        /// This is used to update a location
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] CreateLocationViewModel model)
        {
            var result = _locRepo.UpdateLocation(model, id);
            if (result.successful == false)
            {
                return BadRequest(new APIResponse
                {
                    data = result.location,
                    message = result.message,
                    statusCode = 400
                });
            }
            return Ok(
                new APIResponse
                {
                    data = result.location,
                    message = result.message,
                    statusCode = 200
                });
        }

        // DELETE api/<LocationController>/5
        /// <summary>
        /// This is used to delete a location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _locRepo.DeleteLocationById(id);
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

        // GET api/<LocationController>
        /// <summary>
        /// This is to get all locations within a specific date
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetLocationsWithinDates")]
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
            var result = _locRepo.GetAllLocationsInDateRange(date, date1);

            return Ok(
                new APIResponse
                {
                    data = result.locations,
                    message = result.message,
                    statusCode = 200
                });
        }
    }
}
