using KlasePodataka.EntitetKlase;
using KlasePodataka.InterfejsKlase;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePodataka.Repository
{
    public class SPClanarinaDBKlasa : ISPClanarinaDB
    {
        private string _stringKonekcije;

        public SPClanarinaDBKlasa(string noviStringKonekcije)
        {
            _stringKonekcije = noviStringKonekcije;
        }

        public ClanarinaKlasa DajClanarinuKorisnika(int korisnikID)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajClanarinuKorisnika", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }


            DataRow red = ds.Tables[0].Rows[0];


            return new ClanarinaKlasa
            {

                StatusClanarine = red["StatusClanarine"].ToString(),

                DatumAktivacije = DateOnly.FromDateTime(
                    Convert.ToDateTime(red["DatumAktivacije"])
                ),

                DatumIsteka = DateOnly.FromDateTime(
                    Convert.ToDateTime(red["DatumIsteka"])
                ),
                Cena = Convert.ToDecimal(red["Cena"]),
                Popust = Convert.ToInt32(red["Popust"]),

                ZahtevZaProduzenje = Convert.ToBoolean(red["ZahtevZaProduzenje"])


            };
        }

        public bool KreirajClanarinuKorisnika(int korisnikID, decimal cena, int popust)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("KreirajClanarinuKorisnika", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;
            pomKomanda.Parameters.Add("@Cena", SqlDbType.Decimal).Value = cena;
            pomKomanda.Parameters.Add("@Popust", SqlDbType.Int).Value = popust;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;

        }

        public bool PosaljiZahtevZaProduzenje(int korisnikID)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("PosaljiZahtevZaProduzenje", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;
        }

        public bool PotvrdiUplatuClanarine(int korisnikID, decimal cena, int popust)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("PotvrdiUplatuClanarine", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;
            pomKomanda.Parameters.Add("Cena", SqlDbType.Decimal).Value = cena;
            pomKomanda.Parameters.Add("@Popust", SqlDbType.Int).Value = popust;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;
        }

        public bool OdbijZahtevZaProduzenje(int korisnikID)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("OdbijZahtevZaProduzenje", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;
        }

    }
}
