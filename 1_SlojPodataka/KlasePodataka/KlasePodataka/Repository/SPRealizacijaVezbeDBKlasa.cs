using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using KlasePodataka.EntitetKlase;
using KlasePodataka.InterfejsKlase;

namespace KlasePodataka.Repository
{
    public class SPRealizacijaVezbeDBKlasa : ISPRealizacijaVezbeDB
    {
        private string _stringKonekcije;

        public SPRealizacijaVezbeDBKlasa(string noviStringKonekcije)
        {
            _stringKonekcije = noviStringKonekcije;
        }

        public bool DodajNovuRealizacijuVezbe(RealizacijaVezbeKlasa noviRealizacijaVezbeObjekat)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DodajNovuRealizacijuVezbe", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@NazivVezbe", SqlDbType.NVarChar).Value = noviRealizacijaVezbeObjekat.NazivVezbe;
            pomKomanda.Parameters.Add("@DatumRealizacije", SqlDbType.Date).Value = noviRealizacijaVezbeObjekat.DatumRealizacije;
            pomKomanda.Parameters.Add("@BrojSerija", SqlDbType.Int).Value = noviRealizacijaVezbeObjekat.BrojSerija;
            pomKomanda.Parameters.Add("@BrojPonavljanja", SqlDbType.Int).Value = noviRealizacijaVezbeObjekat.BrojPonavljanja;
            pomKomanda.Parameters.Add("@Tezina", SqlDbType.Decimal).Value = noviRealizacijaVezbeObjekat.Tezina;
            pomKomanda.Parameters.Add("@Trajanje", SqlDbType.Int).Value = noviRealizacijaVezbeObjekat.Trajanje;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = noviRealizacijaVezbeObjekat.Korisnik.KorisnikID;
            pomKomanda.Parameters.Add("@TipVezbeID", SqlDbType.Int).Value = noviRealizacijaVezbeObjekat.TipVezbe.TipVezbeID;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;
        }

        public bool IzmeniRealizacijuVezbe(RealizacijaVezbeKlasa realizacijaVezbeZaIzmenu)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("IzmeniRealizacijuVezbe", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@RealizacijaID", SqlDbType.Int).Value = realizacijaVezbeZaIzmenu.RealizacijaID;
            pomKomanda.Parameters.Add("@NazivVezbe", SqlDbType.NVarChar).Value = realizacijaVezbeZaIzmenu.NazivVezbe;
            pomKomanda.Parameters.Add("@DatumRealizacije", SqlDbType.Date).Value = realizacijaVezbeZaIzmenu.DatumRealizacije;
            pomKomanda.Parameters.Add("@BrojSerija", SqlDbType.Int).Value = realizacijaVezbeZaIzmenu.BrojSerija;
            pomKomanda.Parameters.Add("@BrojPonavljanja", SqlDbType.Int).Value = realizacijaVezbeZaIzmenu.BrojPonavljanja;
            pomKomanda.Parameters.Add("@Tezina", SqlDbType.Decimal).Value = realizacijaVezbeZaIzmenu.Tezina;
            pomKomanda.Parameters.Add("@Trajanje", SqlDbType.Int).Value = realizacijaVezbeZaIzmenu.Trajanje;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = realizacijaVezbeZaIzmenu.Korisnik.KorisnikID;
            pomKomanda.Parameters.Add("@TipVezbeID", SqlDbType.Int).Value = realizacijaVezbeZaIzmenu.TipVezbe.TipVezbeID;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;
        }

        public bool ObrisiRealizacijuVezbe(RealizacijaVezbeKlasa realizacijaZaBrisanje, int korisnikID)
        {

            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("ObrisiRealizacijuVezbe", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@RealizacijaID", SqlDbType.Int).Value = realizacijaZaBrisanje.RealizacijaID;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;
        }

        public DataSet DajRealizacijeVezbiZaDanasnjiDatum(DateOnly datumRealizacije, int korisnikID)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajRealizacijeVezbiZaDanasnjiDatum", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@DatumRealizacije", SqlDbType.Date).Value = datumRealizacije;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }

		public DataSet DajRealizacijuVezbePoID(int realizacijaID, int korisnikID)
		{
			DataSet ds = new DataSet();

			SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);

			pomKonekcija.Open();

			SqlCommand pomKomanda = new SqlCommand("DajRealizacijuVezbePoID", pomKonekcija);

			pomKomanda.CommandType = CommandType.StoredProcedure;

			pomKomanda.Parameters.Add("@RealizacijaID", SqlDbType.Int).Value = realizacijaID;

			pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.SelectCommand = pomKomanda;

			adapter.Fill(ds);

			pomKonekcija.Close();
			pomKonekcija.Dispose();

			return ds;
		}

		public DataSet DajSveRealizacijeVezbi(int korisnikID)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajSveRealizacijeVezbi", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }

        public bool ObrisiSveRealizacijeZaDatum(DateOnly datum, int korisnikID)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("ObrisiSveRealizacijeZaDatum", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@DatumRealizacije", SqlDbType.Date).Value = datum;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;
        }

        public int DajBrojRealizacijaZaMesec(int korisnikID, int mesec, int godina)
        {
            int brojRealizacija = 0;

            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajBrojRealizacijaZaMesec", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;
            pomKomanda.Parameters.Add("@Mesec", SqlDbType.Int).Value = mesec;
            pomKomanda.Parameters.Add("@Godina", SqlDbType.Int).Value = godina;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            brojRealizacija = int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString());

            return brojRealizacija;
        }

        public DataSet DajRealizacijePoDatumu(DateOnly datum, int korisnikID)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajRealizacijePoDatumu", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@DatumRealizacije", SqlDbType.Date).Value = datum;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }

        public DataSet DajRealizacijeZaPeriod(int korisnikID, DateOnly datumOd, DateOnly datumDo)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajRealizacijeZaPeriod", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;
            pomKomanda.Parameters.Add("@DatumOd", SqlDbType.Date).Value = datumOd;
            pomKomanda.Parameters.Add("@DatumDo", SqlDbType.Date).Value = datumDo;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }


    }
}
