using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlasePodataka.EntitetKlase;
using Microsoft.EntityFrameworkCore;



namespace KlasePodataka.KontekstKlasa
{
    public class FitnessCentarKontekst : DbContext
    {
        public FitnessCentarKontekst(DbContextOptions<FitnessCentarKontekst> opcije)
            : base(opcije)
        {

        }

        public DbSet<TipVezbeKlasa> TipoviVezbi { get; set; }
    }
}
