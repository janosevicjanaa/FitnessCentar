using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.ViewModels
{
	public class PromeniLozinkuViewModel
	{
		[Required(ErrorMessage = "Molim Vas unesite trenutnu lozinku.")]
		public string StaraLozinka {  get; set; }

		[Required(ErrorMessage = "Molim Vas unesite novu lozinku.")]
		public string NovaLozinka {  set; get; }


	}
}
