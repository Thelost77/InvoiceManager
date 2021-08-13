using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
