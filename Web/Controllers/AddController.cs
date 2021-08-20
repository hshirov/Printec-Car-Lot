using AutoMapper;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class AddController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public AddController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AddCarViewModel inputModel)
        {
            if (_carService.IsLicensePlateInUse(inputModel.LicensePlate))
            {
                ModelState.AddModelError("LicensePlate", "This license plate is already registered.");
                return View(inputModel);
            }
            if (ModelState.IsValid)
            {
                var model = new Model
                {
                    Name = inputModel.Model,
                    Make = new Make()
                    {
                        Name = inputModel.Make
                    }
                };

                var owner = _mapper.Map<Owner>(inputModel);
                var car = _mapper.Map<Car>(inputModel);

                car.Model = model;
                car.Owner = owner;

                await _carService.Add(car);

                return View();
            }

            return View(inputModel);
        }
    }
}
