using System.ComponentModel.DataAnnotations;

namespace IKIA.PL.ViewModels.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [MinLength(5)]
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Roles { get; set; }
    }
}
