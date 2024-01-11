using System.ComponentModel.DataAnnotations;
using UserManagement.Enumerations;

namespace UserManagement.Models
{
    public class RegistrationRequestModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("\\+(9[976]\\d|8[987530]\\d|6[987]\\d|5[90]\\d|42\\d|3[875]\\d|\r\n2[98654321]\\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|\r\n4[987654310]|3[9643210]|2[70]|7|1)\\d{1,14}$")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression("^(?!\\s+)(?!.*\\s+$)(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[$^*.[\\]{}()?\"!@#%&\\\\,><':;|_~`=+\\- ])[A-Za-z0-9$^*.[\\]{}()?\"!@#%&\\\\,><':;|_~`=+\\- ]{8,256}$", ErrorMessage = "Passwords must be 8 character minimum length, contains at least 1 number, contains at least 1 lowercase letter, contains at least 1 uppercase letter, contains at least 1 special character.")]        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [RegularExpression("^(?!\\s+)(?!.*\\s+$)(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[$^*.[\\]{}()?\"!@#%&\\\\,><':;|_~`=+\\- ])[A-Za-z0-9$^*.[\\]{}()?\"!@#%&\\\\,><':;|_~`=+\\- ]{8,256}$", ErrorMessage = "Confirm passwords 8 character minimum length, contains at least 1 number, contains at least 1 lowercase letter, contains at least 1 uppercase letter, contains at least 1 special character.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public UserType UserRole { get; set; }
    }
}
