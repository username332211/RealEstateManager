using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencija.Model
{
    public class Owner : Person
    {
        public virtual ICollection<RealEstate>? OwnedEstates { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ContractSigned { get; set; }
    }
}
