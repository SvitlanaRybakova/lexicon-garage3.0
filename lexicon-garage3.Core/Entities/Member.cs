using Microsoft.AspNetCore.Identity;

namespace lexicon_garage3.Core.Entities
{
    public class Member: IdentityUser
    {
        // public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonNumber { get; set; }
        //public string UserName => $"{FirstName} {LastName}";

        // nav prop 1-M
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
