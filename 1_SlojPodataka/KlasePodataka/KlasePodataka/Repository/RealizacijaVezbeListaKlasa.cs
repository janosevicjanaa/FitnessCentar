using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlasePodataka.EntitetKlase;

namespace KlasePodataka.Repository
{
    public class RealizacijaVezbeListaKlasa
    {
        private List<RealizacijaVezbeKlasa> _listaRealizacijaVezbe;

        public List<RealizacijaVezbeKlasa> ListaRealizacijaVezbe
        {
            get { return _listaRealizacijaVezbe; }
            set
            {
                if (_listaRealizacijaVezbe != value)
                {
                    _listaRealizacijaVezbe = value;
                }
            }
        }

        public RealizacijaVezbeListaKlasa()
        {
            _listaRealizacijaVezbe = new List<RealizacijaVezbeKlasa>();
        }

        public void DodajElementListe(RealizacijaVezbeKlasa noviRealizacijaVezbeObjekat)
        {
            _listaRealizacijaVezbe.Add(noviRealizacijaVezbeObjekat);
        }

        public void ObrisiElementListe(RealizacijaVezbeKlasa realizacijaVezbeObjekatZaBrisanje)
        {
            _listaRealizacijaVezbe.Remove(realizacijaVezbeObjekatZaBrisanje);
        }

        public void IzmeniElementListe(RealizacijaVezbeKlasa stariRealizacijaVezbeObjekat, RealizacijaVezbeKlasa noviRealizacijaVezbeObjekat)
        {
            int indexStareRealizacije = 0;
            indexStareRealizacije = _listaRealizacijaVezbe.IndexOf(stariRealizacijaVezbeObjekat);
            _listaRealizacijaVezbe.RemoveAt(indexStareRealizacije);
            _listaRealizacijaVezbe.Insert(indexStareRealizacije, noviRealizacijaVezbeObjekat);
        }


    }
}
