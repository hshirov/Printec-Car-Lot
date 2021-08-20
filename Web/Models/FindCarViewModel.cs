using Data.Models;
using System.Collections.Generic;

namespace Web.Models
{
    public class FindCarViewModel
    {
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public IEnumerable<Make> AllMakes { get; set; } 
        public IEnumerable<Model> AllModels { get; set; } 
    }
}
