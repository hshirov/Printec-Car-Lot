using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class APIController : Controller
    {
        private readonly ICarService _carService;
        private readonly IOwnerService _ownerService;
        private readonly IMakeAndModelService _makeAndModelService;
        public APIController(ICarService carService, IOwnerService ownerService, IMakeAndModelService makeAndModelService)
        {
            _carService = carService;
            _ownerService = ownerService;
            _makeAndModelService = makeAndModelService;
        }

        // Cars

        [HttpGet("cars/all")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            var cars = await _carService.GetAll();

            if (cars == null)
            {
                return NotFound();
            }

            return cars.ToArray();
        }

        [HttpGet("cars/get/{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _carService.Get(id);

            if(car == null)
            {
                return NotFound();
            }

            return car;
        }

        // Owners

        [HttpGet("owners/all")]
        public async Task<ActionResult<IEnumerable<Owner>>> GetAllOwners()
        {
            var owners = await _ownerService.GetAll();

            if (owners == null)
            {
                return NotFound();
            }

            return owners.ToArray();
        }

        [HttpGet("owners/get/{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            var owner = await _ownerService.Get(id);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        // Makes

        [HttpGet("makes/all")]
        public async Task<ActionResult<IEnumerable<Make>>> GetAllMakes()
        {
            var makes = await _makeAndModelService.GetAllMakes();

            if (makes == null)
            {
                return NotFound();
            }

            return makes.ToArray();
        }

        [HttpGet("makes/get/{id}")]
        public async Task<ActionResult<Make>> GetMake(int id)
        {
            var makes = await _makeAndModelService.GetMake(id);

            if (makes == null)
            {
                return NotFound();
            }

            return makes;
        }
    }
}
