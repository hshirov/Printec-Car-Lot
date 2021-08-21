using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
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
                .Include(c => c.Owner).FirstOrDefaultAsync(c => c.Id == id && !c.IsArchived);
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Model.Make)
                .Include(c => c.Owner)
                .Where(c => !c.IsArchived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAllByMake(int makeId, int? modelId)
        {
            if(modelId != null)
            {
                return await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Model.Make)
                .Include(c => c.Owner).Where(c => c.Model.Make.Id == makeId && c.Model.Id == modelId && !c.IsArchived)
                .ToListAsync();
            }

            return await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Model.Make)
                .Include(c => c.Owner).Where(c => c.Model.Make.Id == makeId && !c.IsArchived)
                .ToListAsync();
        }

        public bool IsLicensePlateInUse(string licensePlate)
        {
            if(_context.Cars.Where(c => !c.IsArchived).Any(x => x.LicensePlate == licensePlate))
            {
                return true;
            }

            return false;
        }

        public async Task Remove(int id)
        {
            var car = await Get(id);

            _context.Update(car);
            car.IsArchived = true;

            var allCars = await GetAll();
            int carsWithThisModel = allCars.Where(c => c.Model == car.Model).Count();
            int carsWithThisMake = allCars.Where(c => c.Model.Make == car.Model.Make).Count();

            if(carsWithThisMake == 1)
            {
                _context.Remove(car);

                var allArchivedWithMake = await GetAllArchivedByMake(car.Model.Make.Id);
                foreach(var carToDelete in allArchivedWithMake)
                {
                    _context.Update(carToDelete);
                    _context.Remove(carToDelete);
                }

                await _context.SaveChangesAsync();

                await _modelService.RemoveModel(car.Model.Id);
                await _modelService.RemoveMake(car.Model.Make.Id);
            }
            else if(carsWithThisModel == 1)
            {
                _context.Remove(car);

                await _context.SaveChangesAsync();
                await _modelService.RemoveModel(car.Model.Id);
            }

            await _context.SaveChangesAsync();
        }

        private async Task<IEnumerable<Car>> GetAllArchivedByMake(int makeId)
        {
            return await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.Model.Make)
                .Include(c => c.Owner).Where(c => c.Model.Make.Id == makeId && c.IsArchived)
                .ToListAsync();
        }

        public async Task Update(Car car)
        {
            var entityToUpdate = await Get(car.Id);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(car);

            await _context.SaveChangesAsync();
        }
    }
}
