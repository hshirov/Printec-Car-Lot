using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Data
{
    public class MakeAndModelService : IMakeAndModelService
    {
        private readonly ApplicationDbContext _context;

        public MakeAndModelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMake(Make make)
        {
            await _context.AddAsync(make);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Make>> GetAllMakes()
        {
            return await _context.Makes
                 .Include(m => m.Models)
                .ToListAsync();
        }

        public async Task<IEnumerable<Model>> GetAllModelsFromMake(int makeId)
        {
            return await _context.Models
                .Where(m => m.Make.Id == makeId)
                .ToListAsync();
        }

        public async Task<Make> GetMake(int id)
        {
            return await _context.Makes
                .Include(m => m.Models)
                .FirstOrDefaultAsync(m => m.Id == id);
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
