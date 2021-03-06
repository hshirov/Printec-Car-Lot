using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Data
{
    public interface ICarService
    {
        public Task Add(Car car);
        public Task<Car> Get(int id);
        public Task<IEnumerable<Car>> GetAllByMake(int makeId, int? modelId);
        public bool IsLicensePlateInUse(string licensePlate);
        public Task Update(Car car);
        public Task Remove(int id);
        public Task<IEnumerable<Car>> GetAll();
    }
}
