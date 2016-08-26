using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FacilityService
    {
        [Key]
        public int ID { get; set; }
        public HealthService Service { get; set; }
        public string TimeOfDay { get; set; }
    }
}
