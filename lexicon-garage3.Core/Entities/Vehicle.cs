﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexicon_garage3.Core.Entities
{
    public class Vehicle
    {
        public string RegNumber { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime CheckoutTime { get; set; }

        // FK
        public int VehicleTypeId { get; set; }

        // nav props
        public VehicleType VehicleType { get; set; } // 1-M
        public ParkingSpot ParkingSpot { get; set; } // 1-1 
    }
}