using KlasePodataka;
using KlasePodataka.EntitetKlase;
using KlasePodataka.InterfejsKlase;

namespace KlasePoslovneLogike
{
    public class PrijavaKorisnikaKlasa
    {
        private readonly ISPKorisnikDB _spKorisnikDB;

        public PrijavaKorisnikaKlasa(ISPKorisnikDB spKorisnikDB)
        {
            _spKorisnikDB = spKorisnikDB;
        }

        public KorisnikKlasa PrijaviKorisnika(string email, string lozinka)
        {
            KorisnikKlasa korisnik = _spKorisnikDB.DajKorisnikaPoEmailuILozinci(email, lozinka);

            if (korisnik == null)
            {
                return null;
            }

            return korisnik;
        }
    }
}
