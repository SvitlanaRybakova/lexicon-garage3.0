using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexicon_garage3.Core.Entities
{
    public class Member
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonNumber { get; set; }
        public string UserName { get; set; }

        // nav prop 1-M
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
