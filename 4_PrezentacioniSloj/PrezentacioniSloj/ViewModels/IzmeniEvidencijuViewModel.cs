namespace PrezentacioniSloj.ViewModels
{
	public class IzmeniEvidencijuViewModel
	{
        public RealizacijaVezbeViewModel NovaVezba { get; set; }
        public List<RealizacijaVezbeViewModel> Vezbe { get; set; }
        public List<TipVezbeViewModel> TipoviVezbe { get; set; }
        public DateOnly Datum { get; set; }
    }
}
