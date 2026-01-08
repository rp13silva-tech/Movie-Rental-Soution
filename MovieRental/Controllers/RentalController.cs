using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public IActionResult Post([FromBody] Rental.Rental rental)
        {
	        return Ok(_features.Save(rental));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string customerName)
        {
            return Ok(await _features.GetRentalsByCustomerName(customerName));
        }

    }
}
