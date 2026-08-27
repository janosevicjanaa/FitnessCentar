using KlasePodataka.EntitetKlase;
using KlasePodataka.InterfejsKlase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePoslovneLogike
{
    public class UpravljanjeRealizacijamaVezbiKlasa
    {
        private readonly ISPRealizacijaVezbeDB _spRealizacijaVezbeDB;
       

        public UpravljanjeRealizacijamaVezbiKlasa(ISPRealizacijaVezbeDB spRealizacijaVezbeDB)
        {
            _spRealizacijaVezbeDB = spRealizacijaVezbeDB;
            
        }

        public bool IzmeniRealizaciju(KorisnikKlasa prijavljeniKorisnik, RealizacijaVezbeKlasa realizacija)
        {
            if (prijavljeniKorisnik == null || realizacija == null)
            {
                return false;
            }

            if (prijavljeniKorisnik.KorisnikID != realizacija.Korisnik.KorisnikID)
            {
                return false;
            }

            return _spRealizacijaVezbeDB.IzmeniRealizacijuVezbe(realizacija);
        }

        public bool ObrisiRealizaciju(KorisnikKlasa prijavljeniKorisnik, RealizacijaVezbeKlasa realizacija)
        {
            if (prijavljeniKorisnik == null || realizacija == null)
            {
                return false;
            }

            return _spRealizacijaVezbeDB.ObrisiRealizacijuVezbe(realizacija, prijavljeniKorisnik.KorisnikID);
        }

        public bool ObrisiSveRealizacijeZaDatum(DateOnly datum, KorisnikKlasa prijavljeniKorisnik)
        {
            if(prijavljeniKorisnik == null)
            {
                return false;
            }

            return _spRealizacijaVezbeDB.ObrisiSveRealizacijeZaDatum(datum, prijavljeniKorisnik.KorisnikID);
        }

        public List<RealizacijaVezbeKlasa> DajSveRealizacijeKorisnika(KorisnikKlasa prijavljeniKorisnik)
        {
            if (prijavljeniKorisnik == null)
            {
                return null;
            }

            return _spRealizacijaVezbeDB.DajSveRealizacijeVezbi(prijavljeniKorisnik.KorisnikID);
        }

        public List<RealizacijaVezbeKlasa> DajRealizacijeZaDanas(KorisnikKlasa prijavljeniKorisnik)
        {
            if (prijavljeniKorisnik == null)
            {
                return null;
            }

            DateOnly danas = DateOnly.FromDateTime(DateTime.Today);

            return _spRealizacijaVezbeDB.DajRealizacijeVezbiZaDanasnjiDatum(danas, prijavljeniKorisnik.KorisnikID);
        }

        public List<RealizacijaVezbeKlasa> DajRealizacijePoDatumu(KorisnikKlasa prijavljeniKorisnik, DateOnly datum)
        {
            if (prijavljeniKorisnik == null)
            {
                return null;
            }

            return _spRealizacijaVezbeDB.DajRealizacijePoDatumu(datum, prijavljeniKorisnik.KorisnikID);
        }

        public List<RealizacijaVezbeKlasa> DajRealizacijeZaPeriod(KorisnikKlasa prijavljeniKorisnik, DateOnly datumOd, DateOnly datumDo)
        {
            if (prijavljeniKorisnik == null)
            {
                return null;
            }

            if (datumOd > datumDo)
            {
                return null;
            }

            return _spRealizacijaVezbeDB.DajRealizacijeZaPeriod(prijavljeniKorisnik.KorisnikID, datumOd, datumDo);
        }

		public RealizacijaVezbeKlasa DajRealizacijuVezbe(KorisnikKlasa prijavljeniKorisnik, int realizacijaID)
		{
			if (prijavljeniKorisnik == null)
			{
				return null;
			}

			return _spRealizacijaVezbeDB.DajRealizacijuVezbePoID(realizacijaID, prijavljeniKorisnik.KorisnikID);
		}

		public int DajBrojRealizacija(int korisnikID, int mesec, int godina)
		{
			return _spRealizacijaVezbeDB.DajBrojRealizacijaZaMesec(korisnikID, mesec, godina);
		}


	}
}
