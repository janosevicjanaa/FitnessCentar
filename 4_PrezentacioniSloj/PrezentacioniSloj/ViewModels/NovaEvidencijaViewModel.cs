namespace PrezentacioniSloj.ViewModels
{
    public class NovaEvidencijaViewModel
    {
        public DateOnly Datum {  get; set; }

        public RealizacijaVezbeViewModel NovaVezba { get; set; }

        public List<RealizacijaVezbeViewModel> DanasnjeVezbe { get; set; }

        public List<TipVezbeViewModel> TipoviVezbe { get; set; }
    }
}
