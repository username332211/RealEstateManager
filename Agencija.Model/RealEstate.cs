using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agencija.Model
{
    public class RealEstate
    {
        [Key]
        public int ID {  get; set; }
        [Required]
        public string Name { get; set; }
        public EstateType? EstateType { get; set; }
        [Required]
        public string Address { get; set; }
        public int? RoomCount { get; set; }
        [Required]
        public double Price { get; set; }
        [ForeignKey(nameof(Agent))]
        public int? AgentID { get; set; }
        public Agent? Agent { get; set; }
        [ForeignKey(nameof(Owner))]
        public int? OwnerID { get; set; }
        public Owner? Owner { get; set; }
        [ForeignKey(nameof(Neighborhood))]
        public int? NeighborhoodID { get; set; }
        public Neighborhood? Neighborhood { get; set; }
        public double? ExteriorSize {  get; set; }
        [Required]
        public double InteriorSize { get; set; }

    }

    public enum EstateType { House, Apartment, Flat }

}
