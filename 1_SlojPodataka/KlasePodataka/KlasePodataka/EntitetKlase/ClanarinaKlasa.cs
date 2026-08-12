using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePodataka.EntitetKlase
{
    public class ClanarinaKlasa
    {

        private int _clanarinaID;
        private string _statusClanarine;
        private DateOnly _datumAktivacije;
        private DateOnly _datumIsteka;
        private decimal _cena;
        private int _popust;
        private bool _zahtevZaProduzenje;
        private KorisnikKlasa _korisnikObjekat;

        public int ClanarinaID
        {
            get { return _clanarinaID; }
            set { _clanarinaID = value; }
        }

        public string StatusClanarine
        {
            get { return _statusClanarine; }
            set { _statusClanarine = value; }
        }

        public DateOnly DatumAktivacije
        {
            get { return _datumAktivacije; }
            set { _datumAktivacije = value; }
        }

        public DateOnly DatumIsteka
        {
            get { return _datumIsteka; }
            set { _datumIsteka = value; }
        }

        public decimal Cena
        {
            get { return _cena; }
            set { _cena = value; }
        }

        public int Popust
        {
            get { return _popust; }
            set { _popust = value; }
        }

        public bool ZahtevZaProduzenje
        {
            get { return _zahtevZaProduzenje; }
            set { _zahtevZaProduzenje = value; }
        }

        public KorisnikKlasa Korisnik
        {
            get { return _korisnikObjekat; }
            set { _korisnikObjekat = value; }
        }

    }
}
