using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlasePodataka.EntitetKlase;

namespace KlasePodataka.InterfejsKlase
{
    public interface ISPKorisnikDB
    {
        KorisnikKlasa DajKorisnikaPoEmailuILozinci(string email, string lozinka);

        int DodajNovogKorisnika(KorisnikKlasa noviKorisnikObjekat);

        List<KorisnikKlasa> DajSveKorisnike();

        bool IzmeniKorisnika(KorisnikKlasa korisnikZaIzmenu);

        bool IzmeniLozinkuKorisnika(KorisnikKlasa korisnikZaIzmenu, string staraLozinka, string novaLozinka);

        bool ObrisiKorisnika(KorisnikKlasa korisnikZaBrisanje);

        List<ClanarinaKlasa> DajSveKorisnikeSaStatusomClanarine();

        List<ClanarinaKlasa> DajKorisnikePoPrezimenu(string prezime);

        List<ClanarinaKlasa> DajKorisnikePoStatusuClanarine(string statusClanarine);

        ClanarinaKlasa DajProfilKorisnikaZaAdmina(int korisnikID);

        ClanarinaKlasa DajPodatkePocetneStrane(int korisnikID);

        KorisnikKlasa DajPodatkeZaUpravljanjeNalogom(int korisnikID);


    }
}
