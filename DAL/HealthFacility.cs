using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class HealthFacility
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public string Region { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<FacilityService> Services { get; set; }
    }
}
