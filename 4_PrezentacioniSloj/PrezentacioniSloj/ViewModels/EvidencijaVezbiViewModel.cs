namespace PrezentacioniSloj.ViewModels
{
    public class EvidencijaVezbiViewModel
    {
        public DateOnly Datum {  get; set; }
        public List<RealizacijaVezbeViewModel> Vezbe { get; set; }
    }
}
