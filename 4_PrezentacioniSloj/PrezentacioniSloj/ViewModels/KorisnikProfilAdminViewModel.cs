namespace PrezentacioniSloj.ViewModels
{
    public class KorisnikProfilAdminViewModel
    {
        public int KorisnikID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string BrojTelefona { get; set; }

        public DateOnly DatumRodjenja { get; set; }
        public string Pol { get; set; }

        public string StatusClanarine { get; set; }
        public DateOnly DatumAktivacije { get; set; }
        public DateOnly DatumIsteka { get; set; }
        public decimal Cena {  get; set; }
        public int Popust {  get; set; }
        public bool ZahtevZaProduzenje { get; set; }
    }
}
