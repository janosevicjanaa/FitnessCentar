using KlasePodataka.InterfejsKlase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePoslovneLogike
{
    public class AdminUpravljanjeKorisnicimaKlasa
    {
        private readonly ISPKorisnikDB _spKorisnikDB;

        public AdminUpravljanjeKorisnicimaKlasa(ISPKorisnikDB spKorisnikDB)
        {
            _spKorisnikDB = spKorisnikDB;
        }


        public DataSet DajSveKorisnikeSaStatusomClanarine ()
        {

            return _spKorisnikDB.DajSveKorisnikeSaStatusomClanarine();
        }

        public DataSet DajKorisnikePoPrezimenu(string prezime)
        {

            if (string.IsNullOrWhiteSpace(prezime))
            {
                return _spKorisnikDB.DajSveKorisnikeSaStatusomClanarine();
            }

            return _spKorisnikDB.DajKorisnikePoPrezimenu(prezime);
        }

        public DataSet DajKorisnikePoStatusuClanarine(string status)
        {

            if (string.IsNullOrWhiteSpace(status))
            {
                return _spKorisnikDB.DajSveKorisnikeSaStatusomClanarine();
            }

            return _spKorisnikDB.DajKorisnikePoStatusuClanarine(status);
        }

        public DataSet DajProfilKorisnikaZaAdmina(int korisnikID)
        {

            if (korisnikID <= 0)
            {
                return null;
            }

            return _spKorisnikDB.DajProfilKorisnikaZaAdmina(korisnikID);
        }
    }
}
