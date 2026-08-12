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
    public class SPKorisnikDBKlasa : ISPKorisnikDB
    {
        private string _stringKonekcije;

        public SPKorisnikDBKlasa(string noviStringKonekcije)
        {
            _stringKonekcije = noviStringKonekcije;
        }

        public KorisnikKlasa DajKorisnikaPoEmailuILozinci(string email, string lozinka)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajKorisnikaPoEmailuILozinci", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;

            pomKomanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
            pomKomanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = lozinka;

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

            return new KorisnikKlasa
            {
                KorisnikID = Convert.ToInt32(red["KorisnikID"]),
                Ime = red["Ime"].ToString(),
                Prezime = red["Prezime"].ToString(),
                Email = red["Email"].ToString(),
                Lozinka = red["Lozinka"].ToString(),
                BrojTelefona = red["BrojTelefona"].ToString(),
                DatumRodjenja = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRodjenja"])),
                Pol = red["Pol"].ToString(),
                Uloga = red["Uloga"].ToString()
            };

            

        }

        public int DodajNovogKorisnika(KorisnikKlasa noviKorisnikObjekat)
        {

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DodajNovogKorisnika", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;

            pomKomanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = noviKorisnikObjekat.Ime;
            pomKomanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = noviKorisnikObjekat.Prezime;
            pomKomanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = noviKorisnikObjekat.Email;
            pomKomanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = noviKorisnikObjekat.Lozinka;
            pomKomanda.Parameters.Add("@BrojTelefona", SqlDbType.NVarChar).Value = noviKorisnikObjekat.BrojTelefona;
            pomKomanda.Parameters.Add("@DatumRodjenja", SqlDbType.Date).Value = noviKorisnikObjekat.DatumRodjenja;
            pomKomanda.Parameters.Add("@Pol", SqlDbType.NVarChar).Value = noviKorisnikObjekat.Pol;
            

            object rezultat = pomKomanda.ExecuteScalar();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            if (rezultat == null)
            {
                return 0;
            }

            return Convert.ToInt32(rezultat);

        }

        public DataSet DajSveKorisnike()
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajSveKorisnike", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }

        public bool IzmeniKorisnika(KorisnikKlasa korisnikZaIzmenu)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("IzmeniKorisnika", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;

            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikZaIzmenu.KorisnikID;
            pomKomanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = korisnikZaIzmenu.Ime;
            pomKomanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = korisnikZaIzmenu.Prezime;
            pomKomanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = korisnikZaIzmenu.Email;
            pomKomanda.Parameters.Add("@BrojTelefona", SqlDbType.NVarChar).Value = korisnikZaIzmenu.BrojTelefona;
            pomKomanda.Parameters.Add("@DatumRodjenja", SqlDbType.Date).Value = korisnikZaIzmenu.DatumRodjenja;
            pomKomanda.Parameters.Add("@Pol", SqlDbType.NVarChar).Value = korisnikZaIzmenu.Pol;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;

        }

        public bool IzmeniLozinkuKorisnika(KorisnikKlasa korisnikZaIzmenu, string staraLozinka, string novaLozinka)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("IzmeniLozinkuKorisnika", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;

            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikZaIzmenu.KorisnikID;
            pomKomanda.Parameters.Add("@StaraLozinka", SqlDbType.NVarChar).Value = staraLozinka;
            pomKomanda.Parameters.Add("@NovaLozinka", SqlDbType.NVarChar).Value = novaLozinka;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;
        }

        public bool ObrisiKorisnika(KorisnikKlasa korisnikZaBrisanje)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("ObrisiKorisnika", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;

            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikZaBrisanje.KorisnikID;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return brojSlogova > 0;
        }

        public DataSet DajSveKorisnikeSaStatusomClanarine()
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajSveKorisnikeSaStatusomClanarine", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }

        public DataSet DajKorisnikePoPrezimenu(string prezime)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajKorisnikePoPrezimenu", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = prezime;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }

        public DataSet DajKorisnikePoStatusuClanarine(string statusClanarine)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajKorisnikePoStatusuClanarine", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@Status", SqlDbType.NVarChar).Value = statusClanarine;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }

        public DataSet DajProfilKorisnikaZaAdmina(int korisnikID)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajProfilKorisnikaZaAdmina", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }

        public DataSet DajPodatkePocetneStrane(int korisnikID)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajPodatkePocetneStrane", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;
            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }

        public DataSet DajPodatkeZaUpravljanjeNalogom(int korisnikID)
        {
            DataSet ds = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand("DajPodatkeZaUpravljanjeNalogom", pomKonekcija);
            pomKomanda.CommandType = CommandType.StoredProcedure;

            pomKomanda.Parameters.Add("@KorisnikID", SqlDbType.Int).Value = korisnikID;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;

            adapter.Fill(ds);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return ds;
        }
    }
}
