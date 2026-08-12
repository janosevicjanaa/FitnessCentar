using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePodataka.EntitetKlase
{
    public class RealizacijaVezbeKlasa
    {
        private int _realizacijaID;
        private string _nazivVezbe;
        private DateOnly _datumRealizacije;
        private int _brojSerija;
        private int _brojPonavljanja;
        private decimal _tezina;
        private int _trajanje;
        private KorisnikKlasa _korisnikObjekat;
        private TipVezbeKlasa _tipVezbeObjekat;

        public int RealizacijaID
        {
            get { return _realizacijaID; }
            set { _realizacijaID = value; }
        }

        public string NazivVezbe
        {
            get { return _nazivVezbe; }
            set { _nazivVezbe = value; }
        }

        public DateOnly DatumRealizacije
        {
            get { return _datumRealizacije; }
            set { _datumRealizacije = value; }
        }

        public int BrojSerija
        {
            get { return _brojSerija; }
            set { _brojSerija = value; }
        }

        public int BrojPonavljanja
        {
            get { return _brojPonavljanja; }
            set { _brojPonavljanja = value; }
        }

        public decimal Tezina
        {
            get { return _tezina; }
            set { _tezina = value; }
        }

        public int Trajanje
        {
            get { return _trajanje; }
            set { _trajanje = value; }
        }

        public KorisnikKlasa Korisnik
        {
            get { return _korisnikObjekat; }
            set { _korisnikObjekat = value; }
        }

        public TipVezbeKlasa TipVezbe
        {
            get { return _tipVezbeObjekat; }
            set { _tipVezbeObjekat = value; }
        }

    }
}
