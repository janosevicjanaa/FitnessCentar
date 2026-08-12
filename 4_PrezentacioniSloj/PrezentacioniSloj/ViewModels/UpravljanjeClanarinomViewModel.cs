namespace PrezentacioniSloj.ViewModels
{
    public class UpravljanjeClanarinomViewModel
    {
        public string StatusClanarine { get; set; }
        public DateOnly DatumAktivacije { get; set; }
        public DateOnly DatumIsteka { get; set; }
        public decimal Cena { get; set; }
        public int Popust { get; set; }
        public bool ZahtevZaProduzenje {  get; set; }
    }
}
