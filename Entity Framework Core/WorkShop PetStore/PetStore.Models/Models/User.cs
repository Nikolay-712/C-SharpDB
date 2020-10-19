using System.Collections.Generic;

namespace PetStore.Models.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public List<Order> Orders { get; set; }

    }
}
