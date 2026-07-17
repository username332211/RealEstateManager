using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agencija.Model;

namespace Agencija.Model
{
    public class Agent : Person
    {
        [Required]
        public int EstatesSold { get; set; }
        public int? WorkExperience { get; set; }
    }

}

