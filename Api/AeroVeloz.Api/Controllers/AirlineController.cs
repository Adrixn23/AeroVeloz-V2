using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.Services.Flights;
using AeroVeloz.Domain.DomainService.Interfaces.Airlines;
using AeroVeloz.Infraestructure.Persistence.Repositories.Airlines;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AeroVeloz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        public readonly IAirlineDomainService _airlineDomainService;


        public AirlineController(IAirlineDomainService airlineDomainService) // constructor
        {
            _airlineDomainService = airlineDomainService;
        }




        // GET: api/<AirlineController>
        [HttpGet("Register")]
        //public async Task<IActionResult> Register([FromBody]) IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<AirlineController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AirlineController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AirlineController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AirlineController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
