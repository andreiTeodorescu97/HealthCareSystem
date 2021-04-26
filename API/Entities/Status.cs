using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Statuses")]
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Appoinment> Appoinments { get; set; }
    }
}