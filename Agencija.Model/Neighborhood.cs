using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencija.Model
{
    public class Neighborhood
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RealEstate> RealEstates { get; set; }

    }
}
