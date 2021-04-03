using System.Collections.Generic;

namespace API.Entities
{

    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}