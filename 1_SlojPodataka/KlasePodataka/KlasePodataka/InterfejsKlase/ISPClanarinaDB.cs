using KlasePodataka.EntitetKlase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePodataka.InterfejsKlase
{
    public interface ISPClanarinaDB
    {
        ClanarinaKlasa DajClanarinuKorisnika(int korisnikID);

        bool KreirajClanarinuKorisnika(int korisnikID, decimal cena, int popust);

        bool PosaljiZahtevZaProduzenje(int korisnikID);

        bool PotvrdiUplatuClanarine(int korisnikID, decimal cena, int popust);

        bool OdbijZahtevZaProduzenje(int korisnikID);


    }
}
