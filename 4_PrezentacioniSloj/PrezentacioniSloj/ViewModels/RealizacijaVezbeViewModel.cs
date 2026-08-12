namespace PrezentacioniSloj.ViewModels
{
	public class RealizacijaVezbeViewModel
	{
		public int RealizacijaID { get; set; }
		public DateOnly DatumRealizacije {  get; set; }

		public string NazivVezbe { get; set; }

		public string NazivTipa { get; set; }

		public int TipVezbeID { get; set; }

		public int BrojSerija { get; set; }
		public int BrojPonavljanja { get; set; }
		public decimal Tezina {  get; set; }
		public int Trajanje { get; set; }
	}
}
