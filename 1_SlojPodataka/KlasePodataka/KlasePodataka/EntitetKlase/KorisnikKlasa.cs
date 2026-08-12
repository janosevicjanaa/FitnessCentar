using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasePodataka.EntitetKlase
{
    public class KorisnikKlasa
    {
        private int _korisnikID;
        private string _ime;
        private string _prezime;
        private string _email;
        private string _lozinka;
        private string _brojTelefona;
        private DateOnly _datumRodjenja;
        private string _pol;
        private string _uloga;

        public int KorisnikID
        {
            get { return _korisnikID; }
            set { _korisnikID = value; }
        }

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; }
        }

        public string BrojTelefona
        {
            get { return _brojTelefona; }
            set { _brojTelefona = value; }
        }


        public DateOnly DatumRodjenja
        {
            get { return _datumRodjenja; }
            set { _datumRodjenja = value; }
        }

        public string Pol
        {
            get { return _pol; }
            set { _pol = value; }
        }

        public string Uloga
        {
            get { return _uloga; }
            set { _uloga = value; }
        }


    }
}
