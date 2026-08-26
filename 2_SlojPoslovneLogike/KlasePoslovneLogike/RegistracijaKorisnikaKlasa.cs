using KlasePodataka.EntitetKlase;
using KlasePodataka.InterfejsKlase;
using KlasePoslovneLogike.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePoslovneLogike
{
    public class RegistracijaKorisnikaKlasa
    {
        private readonly ISPKorisnikDB _spKorisnikDB;
        private readonly ISPClanarinaDB _spClanarinaDB;
        private readonly RESTServisClanarineKlasa _restServisClanarine;


        public RegistracijaKorisnikaKlasa(ISPKorisnikDB spKorisnikDB,ISPClanarinaDB spClanarinaDB,RESTServisClanarineKlasa restServisClanarine)
        {
            _spKorisnikDB = spKorisnikDB;
            _spClanarinaDB = spClanarinaDB;
            _restServisClanarine = restServisClanarine;
        }


        public int RegistrujKorisnika(KorisnikKlasa korisnik)
        {
            korisnik.Uloga = "Korisnik";

            try
            {
                int korisnikID = _spKorisnikDB.DodajNovogKorisnika(korisnik);
                

                if (korisnikID <= 0)
                {
                    return 0;
                }

                
                decimal cena = _restServisClanarine.DajCenuClanarine();
                

                int popust = 0;

                bool kreiranaClanarina = _spClanarinaDB.KreirajClanarinuKorisnika(korisnikID, cena, popust);
                

                if (!kreiranaClanarina)
                {
                    return 0;
                }


                return korisnikID;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
