using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Data
{
    public class ModelService : IModelService
    {
        private readonly ApplicationDbContext _context;

        public ModelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMake(Make make)
        {
            await _context.AddAsync(make);
            await _context.SaveChangesAsync();
        }

        public async Task AddModel(Model model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public bool TryGetMake(string name, out Make make)
        {
            make = _context.Makes
              .Include(x => x.Models)
              .Where(o => o.Name == name)
              .FirstOrDefault();

            if (make == null)
            {
                return false;
            }

            return true;
        }

        public bool TryGetModel(string name, out Model model)
        {
            model = _context.Models
            .Include(x => x.Make)
            .Where(o => o.Name == name)
            .FirstOrDefault();

            if (model == null)
            {
                return false;
            }

            return true;
        }
    }
}
