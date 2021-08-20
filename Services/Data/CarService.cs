using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Data
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;
        private readonly IOwnerService _ownerService;
        private readonly IMakeAndModelService _modelService;

        public CarService(ApplicationDbContext context, IOwnerService ownerService, IMakeAndModelService modelService)
        {
            _context = context;
            _ownerService = ownerService;
            _modelService = modelService;
        }

        public async Task Add(Car car)
        {
            Owner owner;

            if(!_ownerService.TryGet(car.Owner.FirstName, car.Owner.LastName, out owner))
            {
                // If an owner with this name doesn't exist, create a new one
                owner = new Owner()
                {
                    FirstName = car.Owner.FirstName,
                    LastName = car.Owner.LastName,
                    Cars = new List<Car>()
                };

                await _ownerService.Add(owner);
            }

            // check if model or/and make exist
            Model model;
            Make make;

            bool modelExists = _modelService.TryGetModel(car.Model.Name, out model);
            bool makeExists = _modelService.TryGetMake(car.Model.Make.Name, out make);

            if(!modelExists && !makeExists)
            {
                make = new Make()
                {
                    Name = car.Model.Make.Name,
                    Models = new List<Model>()
                };

                model = new Model()
                {
                    Name = car.Model.Name,
                    Make = make
                };

                make.Models.Add(model);
                await _modelService.AddMake(make);
            }
            else if (!modelExists)
            {
                model = new Model()
                {
                    Name = car.Model.Name,
                    Make = make
                };

                make.Models.Add(model);
            }

            car.Model = model;
            owner.Cars.Add(car);
            car.Owner = owner;

            await _context.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task<Car> Get(int id)
        {
            return await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Model.Make)
                .Include(c => c.Owner).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Model.Make)
                .Include(c => c.Owner)
                .ToListAsync();
        }

        public bool IsLicensePlateInUse(string licensePlate)
        {
            if(_context.Cars.Any(x => x.LicensePlate == licensePlate))
            {
                return true;
            }

            return false;
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
