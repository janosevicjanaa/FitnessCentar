namespace PrezentacioniSloj.ViewModels
{
    public class PocetnaStranaViewModel
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string StatusClanarine { get; set; }

        public DateOnly DanasnjiDatum { get; set; }

		public List<RealizacijaVezbeViewModel> DanasnjeVezbe { get; set; }
		public List<RealizacijaVezbeViewModel> IstorijaVezbi {get; set;}
    }
}
