using DBUtils;
using KlasePodataka.EntitetKlase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePodataka.Repository
{
    public class RealizacijaVezbeDBKlasa : TabelaKlasa
    {
        public RealizacijaVezbeDBKlasa(KonekcijaKlasa novaKonekcija, string noviNazivTabele) : base(novaKonekcija, noviNazivTabele)
        {

        }

        public bool DodajNovuRealizacijuVezbe(RealizacijaVezbeKlasa novaRealizacijaVezbeObjekat)
        {
            string upit = "insert into RealizacijaVezbe " +
            "(NazivVezbe, DatumRealizacije, BrojSerija, BrojPonavljanja, Tezina, Trajanje, KorisnikID, TipVezbeID)" +
            "values ('" +
            novaRealizacijaVezbeObjekat.NazivVezbe + "','" +
            novaRealizacijaVezbeObjekat.DatumRealizacije.ToString("yyyy-MM-dd") + "'," +
            novaRealizacijaVezbeObjekat.BrojSerija + "," +
            novaRealizacijaVezbeObjekat.BrojPonavljanja + "," +
            novaRealizacijaVezbeObjekat.Tezina + "," +
            novaRealizacijaVezbeObjekat.Trajanje + "," +
            novaRealizacijaVezbeObjekat.Korisnik.KorisnikID + "," +
            novaRealizacijaVezbeObjekat.TipVezbe.TipVezbeID + ")";

            return IzvrsiAzuriranje(upit);
        }

        public bool ObrisiRealizacijuVezbe(RealizacijaVezbeKlasa realizacijaVezbeZaBrisanje)
        {
            string upit = "delete from RealizacijaVezbe where RealizacijaID=" + realizacijaVezbeZaBrisanje.RealizacijaID +
                " and KorisnikID=" + realizacijaVezbeZaBrisanje.Korisnik.KorisnikID;

            return IzvrsiAzuriranje(upit);
        }

        public bool IzmeniRealizacijuVezbe(RealizacijaVezbeKlasa realizacijaVezbeZaIzmenu)
        {
            string upit = "update RealizacijaVezbe set " +
            "NazivVezbe='" + realizacijaVezbeZaIzmenu.NazivVezbe + "', " +
            "DatumRealizacije='" + realizacijaVezbeZaIzmenu.DatumRealizacije.ToString("yyyy-MM-dd") + "', " +
            "BrojSerija=" + realizacijaVezbeZaIzmenu.BrojSerija + ", " +
            "BrojPonavljanja=" + realizacijaVezbeZaIzmenu.BrojPonavljanja + ", " +
            "Tezina=" + realizacijaVezbeZaIzmenu.Tezina + ", " +
            "Trajanje=" + realizacijaVezbeZaIzmenu.Trajanje + ", " +
            "TipVezbeID=" + realizacijaVezbeZaIzmenu.TipVezbe.TipVezbeID +
            " where RealizacijaID=" + realizacijaVezbeZaIzmenu.RealizacijaID +
            " and KorisnikID=" + realizacijaVezbeZaIzmenu.Korisnik.KorisnikID;

            return IzvrsiAzuriranje(upit);

        }

        public DataSet DajSveRealizacijeVezbi()
        {
            string upit =
                "SELECT R.RealizacijaID, " +
                "R.NazivVezbe, " +
                "R.DatumRealizacije, " +
                "T.NazivTipa, " +
                "R.BrojSerija, " +
                "R.BrojPonavljanja, " +
                "R.Tezina, " +
                "R.Trajanje " +
                "FROM RealizacijaVezbe R " +
                "INNER JOIN TipVezbe T ON R.TipVezbeID = T.TipVezbeID";

            return DajPodatke(upit);
        }
    }
}
