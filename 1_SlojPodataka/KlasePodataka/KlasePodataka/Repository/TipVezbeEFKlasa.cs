using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlasePodataka.EntitetKlase;
using KlasePodataka.InterfejsKlase;
using KlasePodataka.KontekstKlasa;
using Microsoft.EntityFrameworkCore;

namespace KlasePodataka.Repository
{
    public class TipVezbeEFKlasa : ITipVezbeEF
    {


        private readonly FitnessCentarKontekst _kontekst;

        public TipVezbeEFKlasa(FitnessCentarKontekst kontekst)
        {
            _kontekst = kontekst;
        }


        public List<TipVezbeKlasa> DajSveTipoveVezbi()
        {
            return _kontekst.TipoviVezbi.FromSqlRaw("EXEC DajSveTipoveVezbi").ToList();
        }

    }
}
