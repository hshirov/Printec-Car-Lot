using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Data
{
    public class OwnerService : IOwnerService
    {
        private readonly ApplicationDbContext _context;

        public OwnerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Owner owner)
        {
            await _context.AddAsync(owner);
            await _context.SaveChangesAsync();
        }

        public bool TryGet(string firstName, string lastName, out Owner owner)
        {
            owner = _context.Owners
                .Include(x => x.Cars)
                .Where(o => o.FirstName == firstName && o.LastName == lastName)
                .FirstOrDefault();

            if(owner == null)
            {
                return false;
            }

            return true;
        }
    }
}
