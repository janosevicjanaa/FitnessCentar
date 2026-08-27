using KlasePodataka.EntitetKlase;
using KlasePodataka.InterfejsKlase;


namespace KlasePoslovneLogike
{
    public class AdminUpravljanjeKorisnicimaKlasa
    {
        private readonly ISPKorisnikDB _spKorisnikDB;

        public AdminUpravljanjeKorisnicimaKlasa(ISPKorisnikDB spKorisnikDB)
        {
            _spKorisnikDB = spKorisnikDB;
        }


        public List<ClanarinaKlasa> DajSveKorisnikeSaStatusomClanarine ()
        {

            return _spKorisnikDB.DajSveKorisnikeSaStatusomClanarine();
        }

        public List<ClanarinaKlasa> DajKorisnikePoPrezimenu(string prezime)
        {

            if (string.IsNullOrWhiteSpace(prezime))
            {
                return _spKorisnikDB.DajSveKorisnikeSaStatusomClanarine();
            }

            return _spKorisnikDB.DajKorisnikePoPrezimenu(prezime);
        }

        public List<ClanarinaKlasa> DajKorisnikePoStatusuClanarine(string status)
        {

            if (string.IsNullOrWhiteSpace(status))
            {
                return _spKorisnikDB.DajSveKorisnikeSaStatusomClanarine();
            }

            return _spKorisnikDB.DajKorisnikePoStatusuClanarine(status);
        }

        public ClanarinaKlasa DajProfilKorisnikaZaAdmina(int korisnikID)
        {

            if (korisnikID <= 0)
            {
                return null;
            }

            return _spKorisnikDB.DajProfilKorisnikaZaAdmina(korisnikID);
        }
    }
}
