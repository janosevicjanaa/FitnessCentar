using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KlasePoslovneLogike.PomocneKlase;
using KlasePodataka.InterfejsKlase;

namespace KlasePoslovneLogike
{
    public class ObracunClanarineKlasa
    {
        private readonly ISPRealizacijaVezbeDB _spRealizacijaVezbeDB;
        private readonly RESTServisClanarineKlasa _restServisClanarine;

        public ObracunClanarineKlasa(ISPRealizacijaVezbeDB spRealizacijaVezbeDB, RESTServisClanarineKlasa restServisClanarine)
        {
            _spRealizacijaVezbeDB = spRealizacijaVezbeDB;
            _restServisClanarine = restServisClanarine;
        }


        public decimal IzracunajClanarinu(int korisnikID, int mesec, int godina)
        {
            int brojRealizacija = _spRealizacijaVezbeDB.DajBrojRealizacijaZaMesec(korisnikID, mesec, godina);

            ParametarPopusta parametar = _restServisClanarine.DajParametarPopusta(brojRealizacija);

            decimal cena =_restServisClanarine.DajCenuClanarine();

            return cena - (cena * parametar.ProcenatPopusta / 100m);
        }

		public int IzracunajPopust(int korisnikID, int mesec, int godina)
		{
			int brojRealizacija =_spRealizacijaVezbeDB.DajBrojRealizacijaZaMesec(korisnikID, mesec, godina);

			ParametarPopusta parametar =_restServisClanarine.DajParametarPopusta(brojRealizacija);

			return parametar.ProcenatPopusta;
		}
	}
}
