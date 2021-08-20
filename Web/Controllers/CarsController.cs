using Microsoft.AspNetCore.Mvc;
using Services.Data;
using System.Threading.Tasks;
using Web.Models;
using AutoMapper;
using System.Collections.Generic;
using Web.Common.Helpers;

namespace Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        private readonly ListMapper _listMapper;

        public CarsController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
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
            carViewModel.Make = car.Model.Make.Name;
            carViewModel.Model = car.Model.Name;

            return View(carViewModel);
        }
    }
}
