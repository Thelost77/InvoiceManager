using InvoiceManager.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Wartości pól hasło i potwierdź hasło nie są tożsame.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
