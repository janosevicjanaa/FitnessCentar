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
    public class OgranicenjeZaDodavanjeRealizacijeKlasa
    {
        private readonly ISPClanarinaDB _spClanarinaDB;
        private readonly ISPRealizacijaVezbeDB _sPRealizacijaVezbeDB;

        public OgranicenjeZaDodavanjeRealizacijeKlasa(ISPClanarinaDB spClanarinaDB, ISPRealizacijaVezbeDB sPRealizacijaVezbeDB)
        {
            _spClanarinaDB = spClanarinaDB;
            _sPRealizacijaVezbeDB = sPRealizacijaVezbeDB;
        }

        public string DajStatusClanarine(int korisnikID)
        {
            ClanarinaKlasa clanarina = _spClanarinaDB.DajClanarinuKorisnika(korisnikID);

            if(clanarina == null)
            {
                return "NemaClanarinu";
            }

            return clanarina.StatusClanarine;

            
        }

        private bool DostignutMaksimalanBrojRealizacija(RealizacijaVezbeKlasa realizacija)
        {
            List<RealizacijaVezbeKlasa> realizacije =_sPRealizacijaVezbeDB.DajRealizacijePoDatumu(realizacija.DatumRealizacije,realizacija.Korisnik.KorisnikID);

            int brojRealizacija = realizacije.Count;

            return brojRealizacija >= 20;
        }

        public string DodajRealizacijuAkoJeDozvoljeno(RealizacijaVezbeKlasa realizacija)
        {
            if (realizacija == null || realizacija.Korisnik == null)
            {
                return "Neispravni podaci";
            }

            string status = DajStatusClanarine(realizacija.Korisnik.KorisnikID);

            if (status != "Aktivna")
            {
                return "Neaktivna clanarina";
            }


            if (DostignutMaksimalanBrojRealizacija(realizacija))
            {
                return "Max broj realizacija za izabrani datum";
            }


            bool uspeh = _sPRealizacijaVezbeDB.DodajNovuRealizacijuVezbe(realizacija);


            return uspeh ? "Uspesno" : "Greska";
        }
    }
}
