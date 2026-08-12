using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KlasePoslovneLogike.PomocneKlase
{
    public class RESTServisClanarineKlasa
    {
        private readonly string _url;

        public RESTServisClanarineKlasa(string url)
        {
            _url = url;
        }

        public decimal DajCenuClanarine()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                var podatak = client.GetFromJsonAsync<CenaClanarine>("api/clanarina/cena").Result;

                if (podatak == null)
                {
                    throw new Exception("Cena članarine nije pronađena.");
                }

                return podatak.OsnovnaCena;
            }
        }

        public ParametarPopusta DajParametarPopusta(int brojRealizacija)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var lista = client.GetFromJsonAsync<List<ParametarPopusta>>("api/popust/parametri").Result;

                if (lista == null || lista.Count == 0)
                {
                    throw new Exception("Parametri popusta nisu pronađeni.");
                }

                return lista
                    .Where(p => brojRealizacija >= p.MinBrojRealizacija)
                    .OrderByDescending(p => p.MinBrojRealizacija)
                    .First();
            }
        }
    }
}
