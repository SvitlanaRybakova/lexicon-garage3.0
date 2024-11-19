using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexicon_garage3.Core.Entities
{
    public class ParkingSpot
    {
        public string Id { get; set; } 
        public string Size { get; set; }
        public int ParkingNumber { get; set; }
        public bool IsAvailable { get; set; }
        public int HourRate { get; set; }

        // FK
        public string VehicleRegNumber { get; set; } 

        // nav props 1-1
        public Vehicle Vehicle { get; set; } 
    }
}
