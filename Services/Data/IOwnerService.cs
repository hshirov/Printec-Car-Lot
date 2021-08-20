using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Data
{
    public interface IOwnerService
    {
        public Task Add(Owner owner);
        public Task<Owner> Get(int id);
        public Task<IEnumerable<Owner>> GetAll();
        
        public bool TryGet(string firstName, string lastName, out Owner owner);
    }
}
