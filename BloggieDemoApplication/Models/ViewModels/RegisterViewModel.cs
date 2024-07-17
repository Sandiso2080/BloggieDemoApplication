using System.ComponentModel.DataAnnotations;

namespace BloggieDemoApplication.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username {  get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage ="Password has to be at least 6 character")]
        public string Password { get; set; }
       
    }
}
