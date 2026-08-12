using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.ViewModels
{
    public class PrijavaViewModel
    {
        [Required(ErrorMessage ="Email je obavezan")]
        [RegularExpression( @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage ="Email nije ispravan")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Lozinka je obavezna")]
        [StringLength(40, MinimumLength =6, ErrorMessage = "Lozinka mora imati između 6 i 40 karaktera")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
    }
}
