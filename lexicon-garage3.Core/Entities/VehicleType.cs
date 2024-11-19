using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexicon_garage3.Core.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string VehicleTypeName { get; set; }
        public string VehicleSize { get; set; }
        public int NumOfWheels { get; set; }

        // nav prop M-1
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
