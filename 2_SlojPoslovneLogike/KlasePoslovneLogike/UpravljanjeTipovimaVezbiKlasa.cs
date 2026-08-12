using KlasePodataka.EntitetKlase;
using KlasePodataka.InterfejsKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasePoslovneLogike
{
    public class UpravljanjeTipovimaVezbiKlasa
    {
        private readonly ITipVezbeEF _tipVezbeEF;

        public UpravljanjeTipovimaVezbiKlasa(ITipVezbeEF tipVezbeEF)
        {
            _tipVezbeEF = tipVezbeEF;
        }

        public List<TipVezbeKlasa> DajSveTipoveVezbi()
        {
            return _tipVezbeEF.DajSveTipoveVezbi();
        }
    }
}
