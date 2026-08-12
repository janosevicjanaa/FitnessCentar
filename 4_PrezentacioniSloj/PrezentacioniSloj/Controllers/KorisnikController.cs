using KlasePoslovneLogike;
using PrezentacioniSloj.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using KlasePodataka.EntitetKlase;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PrezentacioniSloj.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly UpravljanjeProfilomKorisnikaKlasa _upravljanjeProfilomKorisnika;
        private readonly SlanjeZahtevaZaProduzenjeClanarineKlasa _slanjeZahtevaZaProduzenjeClanarine;
        private readonly UpravljanjeRealizacijamaVezbiKlasa _upravljanjeRealizacijamaVezbi;
        private readonly UpravljanjeTipovimaVezbiKlasa _upravljanjeTipovimaVezbi;
        private readonly OgranicenjeZaDodavanjeRealizacijeKlasa _ogranicenjeZaDodavanjeRealizacije;
        private readonly ObracunClanarineKlasa _obracunClanarine;

        public KorisnikController(UpravljanjeProfilomKorisnikaKlasa upravljanjeProfilomKorisnika, SlanjeZahtevaZaProduzenjeClanarineKlasa slanjeZahtevaZaProduzenjeClanarine, UpravljanjeRealizacijamaVezbiKlasa upravljanjeRealizacijamaVezbi, UpravljanjeTipovimaVezbiKlasa upravljanjeTipovimaVezbi,
            OgranicenjeZaDodavanjeRealizacijeKlasa ogranicenjeZaDodavanjeRealizacije, ObracunClanarineKlasa obracunClanarine)
        {
            _upravljanjeProfilomKorisnika = upravljanjeProfilomKorisnika;
            _slanjeZahtevaZaProduzenjeClanarine = slanjeZahtevaZaProduzenjeClanarine;
            _upravljanjeRealizacijamaVezbi = upravljanjeRealizacijamaVezbi;
            _upravljanjeTipovimaVezbi = upravljanjeTipovimaVezbi;
            _ogranicenjeZaDodavanjeRealizacije = ogranicenjeZaDodavanjeRealizacije;
            _obracunClanarine = obracunClanarine;
        }

        public IActionResult Index()
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if (korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            DataSet ds = _upravljanjeProfilomKorisnika.DajPodatkeKorisnika(korisnikID.Value);

            if(ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return RedirectToAction("Index", "Prijava");
            }

            PocetnaStranaViewModel model = new PocetnaStranaViewModel();

            model.Ime = ds.Tables[0].Rows[0]["Ime"].ToString();
            model.Prezime = ds.Tables[0].Rows[0]["Prezime"].ToString();
            model.StatusClanarine = ds.Tables[0].Rows[0]["StatusClanarine"].ToString();
            model.DanasnjiDatum = DateOnly.FromDateTime(DateTime.Today);
            model.DanasnjeVezbe = new List<RealizacijaVezbeViewModel>();
            model.IstorijaVezbi = new List<RealizacijaVezbeViewModel>();

            KorisnikKlasa prijavljeniKorisnik = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value
            };

            DataSet dsDanasnjeVezbe = _upravljanjeRealizacijamaVezbi.DajRealizacijeZaDanas(prijavljeniKorisnik);

			if (dsDanasnjeVezbe != null && dsDanasnjeVezbe.Tables.Count > 0 && dsDanasnjeVezbe.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow red in dsDanasnjeVezbe.Tables[0].Rows)
				{
                    model.DanasnjeVezbe.Add(new RealizacijaVezbeViewModel
                    {
                        RealizacijaID = Convert.ToInt32(red["RealizacijaID"]),
                        DatumRealizacije = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRealizacije"])),
                        NazivVezbe = red["NazivVezbe"].ToString(),
                        NazivTipa = red["NazivTipa"].ToString(),
                        BrojSerija = Convert.ToInt32(red["BrojSerija"]),
                        BrojPonavljanja = Convert.ToInt32(red["BrojPonavljanja"]),
                        Tezina = Convert.ToInt32(red["Tezina"]),
                        Trajanje = Convert.ToInt32(red["Trajanje"])
                    });
				}
			}

            DataSet dsIstorijaVezbi = _upravljanjeRealizacijamaVezbi.DajSveRealizacijeKorisnika(prijavljeniKorisnik);

            if(dsIstorijaVezbi != null && dsIstorijaVezbi.Tables.Count > 0 && dsIstorijaVezbi.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow red in dsIstorijaVezbi.Tables[0].Rows)
                {
                    model.IstorijaVezbi.Add(new RealizacijaVezbeViewModel
                    {
                        RealizacijaID = Convert.ToInt32(red["RealizacijaID"]),
                        DatumRealizacije = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRealizacije"])),
                        NazivVezbe = red["NazivVezbe"].ToString(),
                        NazivTipa = red["NazivTipa"].ToString(),
                        BrojSerija = Convert.ToInt32(red["BrojSerija"]),
                        BrojPonavljanja = Convert.ToInt32(red["BrojPonavljanja"]),
                        Tezina = Convert.ToInt32(red["Tezina"]),
                        Trajanje = Convert.ToInt32(red["Trajanje"])
                    });
                }
            }

			return View(model);
        }

        public IActionResult UpravljanjeClanarinom()
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if (korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            ClanarinaKlasa clanarina = _slanjeZahtevaZaProduzenjeClanarine.DajClanarinuKorisnika(korisnikID.Value);

            if (clanarina == null)
            {
                return RedirectToAction("Index");
            }

            int mesec = DateTime.Today.Month;
            int godina = DateTime.Today.Year;

            decimal cenaSaPopustom =_obracunClanarine.IzracunajClanarinu(korisnikID.Value, mesec, godina);

            int procenatPopusta = _obracunClanarine.IzracunajPopust(korisnikID.Value, mesec, godina);

            UpravljanjeClanarinomViewModel model = new UpravljanjeClanarinomViewModel
            {
                StatusClanarine = clanarina.StatusClanarine,
                DatumAktivacije = clanarina.DatumAktivacije,
                DatumIsteka = clanarina.DatumIsteka,
                Cena = cenaSaPopustom,
                Popust = procenatPopusta,
                ZahtevZaProduzenje = clanarina.ZahtevZaProduzenje
            };

            return View(model);

        }

        [HttpPost]
        public IActionResult PosaljiZahtev()
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if (korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            string rezultat = _slanjeZahtevaZaProduzenjeClanarine.PosaljiZahtev(korisnikID.Value);

            switch (rezultat)
            {
                case "Uspesno":
                    TempData["PorukaKorisnik"] = "Zahtev je uspešno poslat.";
                    break;
                case "Prerano":
                    TempData["PorukaKorisnik"] = "Zahtev možete poslati 7 dana pre isteka članarine!";
                    break;
                case "ZahtevVecPoslat":
                    TempData["PorukaKorisnik"] = "Zahtev je već poslat!";
                    break;
                default:
                    TempData["PorukaKorisnik"] = "Došlo je do greške.";
                    break;
            }

            return RedirectToAction("UpravljanjeClanarinom");
        }

        public IActionResult UpravljanjeNalogom()
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            DataSet ds = _upravljanjeProfilomKorisnika.DajPodatkeZaUpravljanjeNalogom(korisnikID.Value);

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return RedirectToAction("Index");
            }

            DataRow red = ds.Tables[0].Rows[0];

            UpravljanjeNalogomViewModel model = new UpravljanjeNalogomViewModel
            {
                Ime = red["Ime"].ToString(),
                Prezime = red["Prezime"].ToString(),
                Email = red["Email"].ToString(),
                BrojTelefona = red["BrojTelefona"].ToString(),
                DatumRodjenja = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRodjenja"])),
                Pol = red["Pol"].ToString()
            };

            return View(model);
        }

        public IActionResult Odjava()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Prijava");
        }

        public IActionResult ObrisiNalog()
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            KorisnikKlasa prijavljeniKorisnik = new KorisnikKlasa()
            {
                KorisnikID = korisnikID.Value
            };

            KorisnikKlasa korisnikZaBrisanje = new KorisnikKlasa()
            {
                KorisnikID = korisnikID.Value
            };

            bool uspeh = _upravljanjeProfilomKorisnika.ObrisiKorisnika(prijavljeniKorisnik, korisnikZaBrisanje);

            if (!uspeh)
            {
                TempData["PorukaBrisanje"] = "Brisanje naloga nije uspelo.";
                return RedirectToAction("UpravljanjeNalogom");
            }

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Prijava");
        }

        public IActionResult IzmeniNalog()
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            DataSet ds = _upravljanjeProfilomKorisnika.DajPodatkeZaUpravljanjeNalogom(korisnikID.Value);

            if(ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return RedirectToAction("Index");
            }

            DataRow red = ds.Tables[0].Rows[0];

            UpravljanjeNalogomViewModel model = new UpravljanjeNalogomViewModel
            {
                Ime = red["Ime"].ToString(),
                Prezime = red["Prezime"].ToString(),
                Email = red["Email"].ToString(),
                BrojTelefona = red["BrojTelefona"].ToString(),
                DatumRodjenja = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRodjenja"])),
                Pol = red["Pol"].ToString()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult IzmeniNalog(UpravljanjeNalogomViewModel model)
        {

			if (!ModelState.IsValid)
            {
                return View(model);
            }

            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            KorisnikKlasa prijavljeniKorisnik = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value
            };

            KorisnikKlasa noviPodaci = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Email = model.Email,
                BrojTelefona = model.BrojTelefona,
                DatumRodjenja = model.DatumRodjenja,
                Pol = model.Pol
            };

            bool uspeh = _upravljanjeProfilomKorisnika.IzmeniKorisnika(prijavljeniKorisnik, noviPodaci);

            if (uspeh)
            {

                return RedirectToAction("UpravljanjeNalogom");
            }

            TempData["PorukaIzmenaPodataka"] = "Došlo je do greške.";

            return View(model);
        }

        public IActionResult PromeniLozinku()
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            return View();
        }

        [HttpPost]
        public IActionResult PromeniLozinku(PromeniLozinkuViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            KorisnikKlasa prijavljeniKorisnik = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value
            };

            bool uspeh = _upravljanjeProfilomKorisnika.IzmeniLozinkuKorisnika(prijavljeniKorisnik, model.StaraLozinka, model.NovaLozinka);

            if (uspeh)
            {
                TempData["PorukaIzmenaLozinke"] = "Lozinka je promenjena.";
                return RedirectToAction("UpravljanjeNalogom");
            }

            TempData["PorukaIzmenaLozinkeGreska"] = "Došlo je do greške, pokušajte ponovo.";
            return View(model);
        }

        public IActionResult NovaEvidencija()
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            NovaEvidencijaViewModel model = new NovaEvidencijaViewModel();

            model.Datum = DateOnly.FromDateTime(DateTime.Today);
            model.NovaVezba = new RealizacijaVezbeViewModel();
            model.DanasnjeVezbe = new List<RealizacijaVezbeViewModel>();
            model.TipoviVezbe = new List<TipVezbeViewModel>();

            List<TipVezbeKlasa> tipovi = _upravljanjeTipovimaVezbi.DajSveTipoveVezbi();

            foreach(var tip in tipovi)
            {
                model.TipoviVezbe.Add(new TipVezbeViewModel
                {
                    TipVezbeID = tip.TipVezbeID,
                    NazivTipa = tip.NazivTipa
                });
            }

            KorisnikKlasa prijavljeniKorisnik = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value
            };

            DataSet dsDanasnjeVezbe = _upravljanjeRealizacijamaVezbi.DajRealizacijeZaDanas(prijavljeniKorisnik);

            if (dsDanasnjeVezbe != null && dsDanasnjeVezbe.Tables.Count > 0 && dsDanasnjeVezbe.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow red in dsDanasnjeVezbe.Tables[0].Rows)
                {
                    model.DanasnjeVezbe.Add(new RealizacijaVezbeViewModel
                    {
                        RealizacijaID = Convert.ToInt32(red["RealizacijaID"]),
                        DatumRealizacije = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRealizacije"])),
                        NazivVezbe = red["NazivVezbe"].ToString(),
                        NazivTipa = red["NazivTipa"].ToString(),
                        BrojSerija = Convert.ToInt32(red["BrojSerija"]),
                        BrojPonavljanja = Convert.ToInt32(red["BrojPonavljanja"]),
                        Tezina = Convert.ToDecimal(red["Tezina"]),
                        Trajanje = Convert.ToInt32(red["Trajanje"])
                    });
                }
            }

                    return View(model);
        }

        [HttpPost]
        public IActionResult SacuvajVezbu(NovaEvidencijaViewModel model, string povratak)
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if (korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            KorisnikKlasa korisnik = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value
            };

            TipVezbeKlasa tipVezbe = new TipVezbeKlasa
            {
                TipVezbeID = model.NovaVezba.TipVezbeID
            };

            RealizacijaVezbeKlasa realizacija = new RealizacijaVezbeKlasa
            {
                RealizacijaID = model.NovaVezba.RealizacijaID,
                DatumRealizacije =model.Datum,
                NazivVezbe = model.NovaVezba.NazivVezbe,
                TipVezbe = tipVezbe,
                BrojSerija = model.NovaVezba.BrojSerija,
                BrojPonavljanja = model.NovaVezba.BrojPonavljanja,
                Tezina = model.NovaVezba.Tezina,
                Trajanje = model.NovaVezba.Trajanje,
                Korisnik = korisnik
            };

            if(realizacija.RealizacijaID > 0)
            {
                bool izmenjeno = _upravljanjeRealizacijamaVezbi.IzmeniRealizaciju(korisnik, realizacija);
            }
            else
            {
				string rezultat = _ogranicenjeZaDodavanjeRealizacije.DodajRealizacijuAkoJeDozvoljeno(realizacija);

				if (rezultat == "Uspesno")
				{
					TempData["PorukaRealizacije"] = "Vežba je dodata.";
				}
				else
				{
					TempData["PorukaRealizacije"] = rezultat;
				}
			}



            if (povratak == "IzmeniEvidenciju")
            {
                return RedirectToAction("IzmeniEvidenciju", new { datum = model.Datum });
            }

            return RedirectToAction("NovaEvidencija");

        }

        public IActionResult EvidencijaVezbi( DateOnly datum)
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            KorisnikKlasa prijavljeniKorisnik = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value
            };

            EvidencijaVezbiViewModel model = new EvidencijaVezbiViewModel();

            model.Datum = datum;
            model.Vezbe = new List<RealizacijaVezbeViewModel>();

            DataSet ds = _upravljanjeRealizacijamaVezbi.DajRealizacijePoDatumu(prijavljeniKorisnik, datum);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow red in ds.Tables[0].Rows)
                {
                    model.Vezbe.Add(new RealizacijaVezbeViewModel
                    {
                        RealizacijaID = Convert.ToInt32(red["RealizacijaID"]),
                        DatumRealizacije = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRealizacije"])),
                        NazivVezbe = red["NazivVezbe"].ToString(),
                        NazivTipa = red["NazivTipa"].ToString(),
                        BrojSerija = Convert.ToInt32(red["BrojSerija"]),
                        BrojPonavljanja = Convert.ToInt32(red["BrojPonavljanja"]),
                        Tezina = Convert.ToInt32(red["Tezina"]),
                        Trajanje = Convert.ToInt32(red["Trajanje"])
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult SacuvajSve()
        {
            return RedirectToAction("EvidencijaVezbi", new {datum = DateOnly.FromDateTime(DateTime.Today)});
        }

        [HttpPost]
        public IActionResult IzbrisiSve(DateOnly datum, string povratak)
        {
			int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

			if (korisnikID == null)
			{
				return RedirectToAction("Index", "Prijava");
			}

			KorisnikKlasa korisnik = new KorisnikKlasa
			{
				KorisnikID = korisnikID.Value
			};

            bool obrisano = _upravljanjeRealizacijamaVezbi.ObrisiSveRealizacijeZaDatum(datum, korisnik);

            if (obrisano)
            {
                TempData["PorukaIzbrisiSve"] = "Realizacije za današnji datum su izbrisane.";
            }
            else
            {
                TempData["PorukaIzbrisiSve"] = "Greška pri brisanju realizacija.";
            }

            if(povratak == "NovaEvidencija")
            {
				return RedirectToAction("NovaEvidencija");
			}

            return RedirectToAction("Index");
		}

        [HttpGet]
        public IActionResult IzmeniVezbu(int realizacijaID, DateOnly? datum, string povratak)
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if (korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            KorisnikKlasa prijavljeniKorisnik = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value
            };

            NovaEvidencijaViewModel model = new NovaEvidencijaViewModel();

            if (datum.HasValue)
            {
                model.Datum = datum.Value;
            }
            else
            {
                model.Datum = DateOnly.FromDateTime(DateTime.Today);
            }
            model.NovaVezba = new RealizacijaVezbeViewModel();
            model.DanasnjeVezbe = new List<RealizacijaVezbeViewModel>();
            model.TipoviVezbe = new List<TipVezbeViewModel>();

            List<TipVezbeKlasa> tipovi = _upravljanjeTipovimaVezbi.DajSveTipoveVezbi();

            foreach (var tip in tipovi)
            {
                model.TipoviVezbe.Add(new TipVezbeViewModel
                {
                    TipVezbeID = tip.TipVezbeID,
                    NazivTipa = tip.NazivTipa
                });
            }

            DataSet ds = _upravljanjeRealizacijamaVezbi.DajRealizacijuVezbe(prijavljeniKorisnik, realizacijaID);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow red = ds.Tables[0].Rows[0];

                model.NovaVezba = new RealizacijaVezbeViewModel
                {
                    RealizacijaID = Convert.ToInt32(red["RealizacijaID"]),
                    DatumRealizacije = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRealizacije"])),
                    NazivVezbe = red["NazivVezbe"].ToString(),
                    TipVezbeID = Convert.ToInt32(red["TipVezbeID"]),
                    NazivTipa = red["NazivTipa"].ToString(),
                    BrojSerija = Convert.ToInt32(red["BrojSerija"]),
                    BrojPonavljanja = Convert.ToInt32(red["BrojPonavljanja"]),
                    Tezina = Convert.ToDecimal(red["Tezina"]),
                    Trajanje = Convert.ToInt32(red["Trajanje"])
                };
            }

            DataSet dsDanasnjeVezbe = _upravljanjeRealizacijamaVezbi.DajRealizacijePoDatumu(prijavljeniKorisnik, model.Datum);

            if (dsDanasnjeVezbe != null && dsDanasnjeVezbe.Tables.Count > 0)
            {
                foreach (DataRow red in dsDanasnjeVezbe.Tables[0].Rows)
                {
                    model.DanasnjeVezbe.Add(
                        new RealizacijaVezbeViewModel
                        {
                            RealizacijaID = Convert.ToInt32(red["RealizacijaID"]),
                            DatumRealizacije = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRealizacije"])),
                            NazivVezbe =red["NazivVezbe"].ToString(),
                            NazivTipa =red["NazivTipa"].ToString(),
                            BrojSerija = Convert.ToInt32(red["BrojSerija"]),
                            BrojPonavljanja = Convert.ToInt32(red["BrojPonavljanja"]),
                            Tezina = Convert.ToDecimal(red["Tezina"]),
                            Trajanje = Convert.ToInt32(red["Trajanje"])
                        });
                }
            }

            if (povratak == "IzmeniEvidenciju")
            {
                return View("IzmeniEvidenciju", new IzmeniEvidencijuViewModel
                {
                    Datum = model.Datum,
                    Vezbe = model.DanasnjeVezbe,
                    NovaVezba = model.NovaVezba,
                    TipoviVezbe = model.TipoviVezbe
                });
            }

            return View("NovaEvidencija", model);
        }

        [HttpPost]
        public IActionResult IzbrisiVezbu(int realizacijaID, DateOnly datum, string povratak)
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if (korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            KorisnikKlasa korisnik = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value
            };

            RealizacijaVezbeKlasa realizacija = new RealizacijaVezbeKlasa
                {
                    RealizacijaID = realizacijaID
                };

            bool obrisano =  _upravljanjeRealizacijamaVezbi.ObrisiRealizaciju(korisnik, realizacija);

            if (obrisano)
            {
                TempData["PorukaIzbrisi"] = "Vežba je obrisana.";
            }
            else
            {
                TempData["PorukaIzbrisi"] = "Greška pri brisanju.";
            }

            if (povratak == "IzmeniEvidenciju")
            {
                return RedirectToAction("IzmeniEvidenciju", new { datum = datum });
            }

            return RedirectToAction("NovaEvidencija");
        }

        public IActionResult ParametarskaStampa()
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            ParametarskaStampaViewModel model = new ParametarskaStampaViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult ParametarskaStampa(ParametarskaStampaViewModel model)
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if(korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            KorisnikKlasa prijavljeniKorisnik = new KorisnikKlasa
            {
                KorisnikID = korisnikID.Value
            };

            DataSet ds = _upravljanjeRealizacijamaVezbi.DajRealizacijeZaPeriod(prijavljeniKorisnik, model.DatumOd, model.DatumDo);

            if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow red in ds.Tables[0].Rows)
                {
                    model.Realizacije.Add(new RealizacijaVezbeViewModel
                    {
                        RealizacijaID = Convert.ToInt32(red["RealizacijaID"]),
                        DatumRealizacije = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRealizacije"])),
                        NazivVezbe = red["NazivVezbe"].ToString(),
                        NazivTipa = red["NazivTipa"].ToString(),
                        BrojSerija = Convert.ToInt32(red["BrojSerija"]),
                        BrojPonavljanja = Convert.ToInt32(red["BrojPonavljanja"]),
                        Tezina = Convert.ToDecimal(red["Tezina"]),
                        Trajanje = Convert.ToInt32(red["Trajanje"])
                    });

                    
                }
            }

			return View(model);
		}

		[HttpGet]
		public IActionResult IzmeniEvidenciju(DateOnly datum)
		{
			int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

			if (korisnikID == null)
			{
				return RedirectToAction("Index", "Prijava");
			}

			KorisnikKlasa korisnik = new KorisnikKlasa
			{
				KorisnikID = korisnikID.Value
			};

			IzmeniEvidencijuViewModel model = new IzmeniEvidencijuViewModel();

			model.Datum = datum;
			model.Vezbe = new List<RealizacijaVezbeViewModel>();

			DataSet ds = _upravljanjeRealizacijamaVezbi.DajRealizacijePoDatumu(korisnik, datum);

			if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow red in ds.Tables[0].Rows)
				{
					model.Vezbe.Add( new RealizacijaVezbeViewModel
						{
							RealizacijaID = Convert.ToInt32(red["RealizacijaID"]),
                            DatumRealizacije = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRealizacije"])),
                            NazivVezbe = red["NazivVezbe"].ToString(),
                            NazivTipa = red["NazivTipa"].ToString(),
                            BrojSerija = Convert.ToInt32(red["BrojSerija"]),
                            BrojPonavljanja = Convert.ToInt32(red["BrojPonavljanja"]),
                            Tezina = Convert.ToDecimal(red["Tezina"]),
                            Trajanje = Convert.ToInt32(red["Trajanje"])
						});
				}
			}

			return View(model);
		}

	}
}
