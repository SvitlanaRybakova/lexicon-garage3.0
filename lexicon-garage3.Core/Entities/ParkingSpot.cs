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
        public int ParkingNumber { get; set; } //TODO: maybe this should be unique? right now it's possible to have several spots with the same parkingnumber
        public bool IsAvailable { get; set; }
        public int HourRate { get; set; }

        // FK
        public string? RegNumber { get; set; } 

        // nav props 1-1
        public Vehicle? Vehicle { get; set; } 
    }
}
