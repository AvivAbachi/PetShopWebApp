using System.ComponentModel.DataAnnotations;

namespace PetShopWebApp.Models
{
    public class LoginUserView
    {
        [Required(ErrorMessage = "Please enter your Username")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Please enter your Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
