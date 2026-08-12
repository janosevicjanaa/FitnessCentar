namespace PrezentacioniSloj.ViewModels
{
	public class ParametarskaStampaViewModel
	{
		public DateOnly DatumOd {  get; set; }
		public DateOnly DatumDo { get; set; }
		public List<RealizacijaVezbeViewModel> Realizacije { get; set; } = new List<RealizacijaVezbeViewModel>();
	}
}
