using Microsoft.AspNetCore.Identity;

namespace EReceipt.DAL.Entities
{
    public class User : IdentityUser
    {
        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }

        public Pharmacy Pharmacy { get; set; }
    }
}
