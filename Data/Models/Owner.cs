using System.Collections.Generic;

namespace Data.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Car> Cars { get; set; } 
    }
}