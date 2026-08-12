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
    public class UpravljanjeProfilomKorisnikaKlasa
    {
        private readonly ISPKorisnikDB _spKorisnikDB;


        public UpravljanjeProfilomKorisnikaKlasa(ISPKorisnikDB spKorisnikDB)
        {
            _spKorisnikDB = spKorisnikDB;
        }

        public bool IzmeniKorisnika(KorisnikKlasa prijavljeniKorisnik, KorisnikKlasa noviPodaci)
        {
            
            if (prijavljeniKorisnik.KorisnikID != noviPodaci.KorisnikID)
            {
                return false;
            }


            return _spKorisnikDB.IzmeniKorisnika(noviPodaci);
        }

        public bool IzmeniLozinkuKorisnika(KorisnikKlasa prijavljeniKorisnik, string staraLozinka, string novaLozinka)
        {
            if (prijavljeniKorisnik == null)
            {
                return false;
            }

            if (prijavljeniKorisnik.KorisnikID <= 0)
            {
                return false;
            }

            if (staraLozinka == novaLozinka)
            {
                return false;
            }


            return _spKorisnikDB.IzmeniLozinkuKorisnika(prijavljeniKorisnik,staraLozinka,novaLozinka);
        }

        public bool ObrisiKorisnika(KorisnikKlasa prijavljeniKorisnik, KorisnikKlasa korisnikZaBrisanje)
        {
            if (prijavljeniKorisnik.KorisnikID != korisnikZaBrisanje.KorisnikID)
            {
                return false;
            }


            return _spKorisnikDB.ObrisiKorisnika(korisnikZaBrisanje);
        }

        public DataSet DajPodatkeKorisnika(int korisnikID)
        {
            return _spKorisnikDB.DajPodatkePocetneStrane(korisnikID);
        }

        public DataSet DajPodatkeZaUpravljanjeNalogom(int korisnikID)
        {
            return _spKorisnikDB.DajPodatkeZaUpravljanjeNalogom(korisnikID);
        }
    }
}

