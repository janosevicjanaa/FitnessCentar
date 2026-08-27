using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlasePodataka.EntitetKlase;

namespace KlasePodataka.InterfejsKlase
{
    public interface ISPRealizacijaVezbeDB
    {
        bool DodajNovuRealizacijuVezbe(RealizacijaVezbeKlasa noviRealizacijaVezbeObjekat);

        bool IzmeniRealizacijuVezbe(RealizacijaVezbeKlasa realizacijaVezbeZaIzmenu);

        bool ObrisiRealizacijuVezbe(RealizacijaVezbeKlasa realizacijaZaBrisanje, int korisnikID);

        List<RealizacijaVezbeKlasa> DajRealizacijeVezbiZaDanasnjiDatum(DateOnly datumRealizacije, int korisnikID);

        RealizacijaVezbeKlasa DajRealizacijuVezbePoID(int realizacijaID, int korisnikID);

        List<RealizacijaVezbeKlasa> DajSveRealizacijeVezbi(int korisnikID);

        bool ObrisiSveRealizacijeZaDatum(DateOnly datum, int korisnikID);

        int DajBrojRealizacijaZaMesec(int korisnikID, int mesec, int godina);

        List<RealizacijaVezbeKlasa> DajRealizacijePoDatumu(DateOnly datum, int korisnikID);

        List<RealizacijaVezbeKlasa> DajRealizacijeZaPeriod(int korisnikID, DateOnly datumOd, DateOnly datumDo);


    }
}
