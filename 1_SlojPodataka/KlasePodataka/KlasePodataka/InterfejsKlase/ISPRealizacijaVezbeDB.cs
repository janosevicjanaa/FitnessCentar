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

        DataSet DajRealizacijeVezbiZaDanasnjiDatum(DateOnly datumRealizacije, int korisnikID);

        DataSet DajRealizacijuVezbePoID(int realizacijaID, int korisnikID);

        DataSet DajSveRealizacijeVezbi(int korisnikID);

        bool ObrisiSveRealizacijeZaDatum(DateOnly datum, int korisnikID);

        int DajBrojRealizacijaZaMesec(int korisnikID, int mesec, int godina);

        DataSet DajRealizacijePoDatumu(DateOnly datum, int korisnikID);

        DataSet DajRealizacijeZaPeriod(int korisnikID, DateOnly datumOd, DateOnly datumDo);


    }
}
