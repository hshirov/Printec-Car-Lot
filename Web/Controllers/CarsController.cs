using Microsoft.AspNetCore.Mvc;
using Services.Data;
using System.Threading.Tasks;
using Web.Models;
using AutoMapper;
using Web.Common.Helpers;
using Data.Models;

namespace Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMakeAndModelService _makeAndModelService;
        private readonly IMapper _mapper;
        private readonly ListMapper _listMapper;

        public CarsController(ICarService carService, IMakeAndModelService makeAndModelService, IMapper mapper)
        {
            _carService = carService;
            _makeAndModelService = makeAndModelService;
            _mapper = mapper;
            _listMapper = new ListMapper(mapper);
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAll();
            var viewModel = _listMapper.CarsToCarViewModels(cars);

            return View(viewModel);
        }


        public async Task<IActionResult> Search(int makeId, int? modelId)
        {
            var cars = await _carService.GetAllByMake(makeId, modelId);
            var viewModel = _listMapper.CarsToCarViewModels(cars);

            return View("Index", viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.Get(id);
            var carViewModel = _mapper.Map<CarIndexViewModel>(car);

            return View(carViewModel);
        }

        public IActionResult Add()
        {
            ViewBag.SuccessContact = TempData["SuccessContact"];

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddCarViewModel inputModel)
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
                TempData["SuccessContact"] = true;

                return RedirectToAction(nameof(this.Add));
            }

            return View(inputModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.Get(id);
            var carViewModel = _mapper.Map<EditCarViewModel>(car);

            ViewBag.SuccessContact = TempData["SuccessContact"];

            return View(carViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCarViewModel inputModel)
        {
            if (_carService.IsLicensePlateInUse(inputModel.LicensePlate) && inputModel.LicensePlate != _carService.Get(inputModel.Id).Result.LicensePlate)
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

                await _carService.Update(car);
                TempData["SuccessContact"] = true;

                return RedirectToAction(nameof(this.Edit));
            }

            return View(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            await _carService.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
