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

        DataSet DajSveKorisnike();

        bool IzmeniKorisnika(KorisnikKlasa korisnikZaIzmenu);

        bool IzmeniLozinkuKorisnika(KorisnikKlasa korisnikZaIzmenu, string staraLozinka, string novaLozinka);

        bool ObrisiKorisnika(KorisnikKlasa korisnikZaBrisanje);

        DataSet DajSveKorisnikeSaStatusomClanarine();

        DataSet DajKorisnikePoPrezimenu(string prezime);

        DataSet DajKorisnikePoStatusuClanarine(string statusClanarine);

        DataSet DajProfilKorisnikaZaAdmina(int korisnikID);

        DataSet DajPodatkePocetneStrane(int korisnikID);

        DataSet DajPodatkeZaUpravljanjeNalogom(int korisnikID);


    }
}
