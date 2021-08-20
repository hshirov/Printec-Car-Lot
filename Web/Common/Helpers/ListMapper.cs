using AutoMapper;
using Data.Models;
using System.Collections.Generic;
using Web.Models;

namespace Web.Common.Helpers
{
    public class ListMapper
    {
        private readonly IMapper _mapper;

        public ListMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CarsViewModel CarsToCarViewModels(IEnumerable<Car> cars)
        {
            var carViewModels = new List<CarIndexViewModel>();

            foreach (var car in cars)
            {
                var modelToAdd = _mapper.Map<CarIndexViewModel>(car);
                modelToAdd.Make = car.Model.Make.Name;
                modelToAdd.Model = car.Model.Name;

                carViewModels.Add(modelToAdd);
            }

            CarsViewModel viewModel = new CarsViewModel()
            {
                Cars = carViewModels
            };

            return viewModel;
        }
    }
}
