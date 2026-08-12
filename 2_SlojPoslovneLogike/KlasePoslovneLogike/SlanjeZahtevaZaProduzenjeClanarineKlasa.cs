using KlasePodataka.EntitetKlase;
using KlasePodataka.InterfejsKlase;
using KlasePoslovneLogike.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePoslovneLogike
{
    public class SlanjeZahtevaZaProduzenjeClanarineKlasa
    {
        private readonly ISPClanarinaDB _spClanarinaDB;
        private readonly RESTServisClanarineKlasa _restServisClanarine;

        public SlanjeZahtevaZaProduzenjeClanarineKlasa(ISPClanarinaDB spClanarinaDB, RESTServisClanarineKlasa restServisClanarine)
        {
            _spClanarinaDB = spClanarinaDB;
            _restServisClanarine = restServisClanarine;
        }

        public string PosaljiZahtev(int korisnikID)
        {
            ClanarinaKlasa clanarina = _spClanarinaDB.DajClanarinuKorisnika(korisnikID);

            if (clanarina == null)
            {
                return "NemaClanarinu";
            }

            if (clanarina.ZahtevZaProduzenje)
            {
                return "ZahtevVecPoslat";
            }

            DateOnly danas = DateOnly.FromDateTime(DateTime.Today);

            if (clanarina.DatumIsteka > danas.AddDays(20))
            {
                return "Prerano";
            }

            bool uspeh = _spClanarinaDB.PosaljiZahtevZaProduzenje(korisnikID);

            return uspeh ? "Uspesno" : "Greska";
        }

        public string OdbijZahtev(int korisnikID)
        {
            ClanarinaKlasa clanarina = _spClanarinaDB.DajClanarinuKorisnika(korisnikID);

            if (clanarina == null)
            {
                return "NemaClanarinu";
            }

            if (!clanarina.ZahtevZaProduzenje)
            {
                return "NemaZahteva";
            }

            bool uspeh = _spClanarinaDB.OdbijZahtevZaProduzenje(korisnikID);

            return uspeh ? "Uspesno" : "Greska";
        }

        public string PotvrdiUplatu(int korisnikID)
        {
            ClanarinaKlasa clanarina =
                _spClanarinaDB.DajClanarinuKorisnika(korisnikID);


            if (clanarina == null)
            {
                return "NemaClanarinu";
            }


            if (!clanarina.ZahtevZaProduzenje)
            {
                return "NemaZahteva";
            }


            decimal osnovnaCena = _restServisClanarine.DajCenuClanarine();


            int pocetniPopust = 0;


            bool uspeh = _spClanarinaDB.PotvrdiUplatuClanarine(korisnikID, osnovnaCena, pocetniPopust);


            return uspeh ? "Uspesno" : "Greska";
        }

        public ClanarinaKlasa DajClanarinuKorisnika(int korisnikID)
        {
            ClanarinaKlasa clanarina = _spClanarinaDB.DajClanarinuKorisnika(korisnikID);

            if (clanarina == null)
            {
                return null;
            }

            clanarina.Cena = _restServisClanarine.DajCenuClanarine();

            return clanarina;
        }
    }
}
