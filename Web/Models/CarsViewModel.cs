using System.Collections.Generic;

namespace Web.Models
{
    public class CarsViewModel
    {
        public IEnumerable<CarIndexViewModel> Cars { get; set; }
    }
}
