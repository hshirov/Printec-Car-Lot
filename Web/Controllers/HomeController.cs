using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMakeAndModelService _makeAndModelService;

        public HomeController(ILogger<HomeController> logger, IMakeAndModelService makeAndModelService)
        {
            _logger = logger;
            _makeAndModelService = makeAndModelService;
        }

        public async Task<IActionResult> Index()
        {
            FindCarViewModel model = new FindCarViewModel()
            {
                AllMakes = await _makeAndModelService.GetAllMakes(),
                AllModels = new List<Model>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FindCarViewModel inputModel)
        {
            inputModel.AllMakes = await _makeAndModelService.GetAllMakes();
            inputModel.AllModels = await _makeAndModelService.GetAllModelsFromMake(inputModel.MakeId);

            return View(inputModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
