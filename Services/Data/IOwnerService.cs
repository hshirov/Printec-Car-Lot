using Data.Models;
using System.Threading.Tasks;

namespace Services.Data
{
    public interface IOwnerService
    {
        public Task Add(Owner owner);
        public bool TryGet(string firstName, string lastName, out Owner owner);
    }
}
