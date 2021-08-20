using Data.Models;
using System.Threading.Tasks;

namespace Services.Data
{
    public interface IModelService
    {
        public bool TryGetModel(string name, out Model model);
        public bool TryGetMake(string name, out Make make);
        public Task AddMake(Make make);
        public Task AddModel(Model model);
    }
}
