using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Data
{
    public interface IMakeAndModelService
    {
        public bool TryGetModel(string name, out Model model);
        public bool TryGetMake(string name, out Make make);
        public Task AddMake(Make make);
        public Task<IEnumerable<Make>> GetAllMakes();
        public Task<IEnumerable<Model>> GetAllModelsFromMake(int makeId);
        public Task<Make> GetMake(int id);
    }
}
