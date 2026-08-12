using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.ViewModels
{
	public class RegistracijaViewModel
	{
		[Required(ErrorMessage = "Ime je obavezno!")]
		public string Ime { get; set; }

		[Required(ErrorMessage ="Prezime je obavezno!")]
		public string Prezime { get; set; }

		[Required(ErrorMessage = "Email je obavezan!")]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email nije ispravan")]
		public string Email { get; set; }

		[Required(ErrorMessage ="Lozinka je obavezna!")]
		[StringLength(40, MinimumLength = 6, ErrorMessage = "Lozinka mora imati između 6 i 40 karaktera")]
		[DataType(DataType.Password)]
		public string Lozinka {  get; set; }

		[Required(ErrorMessage ="Broj telefona je obavezan!")]
		[RegularExpression(@"^(\+381|0)[0-9]{8,9}$", ErrorMessage = "Broj telefona nije ispravan")]
		public string BrojTelefona {  get; set; }

		[Required(ErrorMessage = "Datum rođenja je obavezan!")]
		public DateOnly DatumRodjenja {  get; set; }

		[Required(ErrorMessage ="Pol je obavezan!")]
		public string Pol { get; set; }
	}
}
