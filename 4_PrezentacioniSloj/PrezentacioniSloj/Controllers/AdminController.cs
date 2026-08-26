using KlasePodataka.InterfejsKlase;
using KlasePoslovneLogike;
using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.ViewModels;
using System.Data;


namespace PrezentacioniSloj.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminUpravljanjeKorisnicimaKlasa _adminUpravljanjeKorisnicima;
        private readonly SlanjeZahtevaZaProduzenjeClanarineKlasa _slanjeZahtevaZaProduzenjeClanarine;
        private readonly ObracunClanarineKlasa _obracunClanarine;

        public AdminController(AdminUpravljanjeKorisnicimaKlasa adminUpravljanjeKorisnicima, SlanjeZahtevaZaProduzenjeClanarineKlasa slanjeZahtevaZaProduzenjeClanarine, ObracunClanarineKlasa obracunClanarine)
        {
            _adminUpravljanjeKorisnicima = adminUpravljanjeKorisnicima;
            _slanjeZahtevaZaProduzenjeClanarine = slanjeZahtevaZaProduzenjeClanarine;
            _obracunClanarine = obracunClanarine;
        }

        public IActionResult Index(string prezimePretraga, string statusClanarineFilter)
        {
            int? korisnikID = HttpContext.Session.GetInt32("KorisnikID");

            if (korisnikID == null)
            {
                return RedirectToAction("Index", "Prijava");
            }

            DataSet ds;

            if (!string.IsNullOrWhiteSpace(prezimePretraga))
            {
                ds = _adminUpravljanjeKorisnicima.DajKorisnikePoPrezimenu(prezimePretraga);
            }
            else if (!string.IsNullOrWhiteSpace(statusClanarineFilter))
            {
                ds = _adminUpravljanjeKorisnicima.DajKorisnikePoStatusuClanarine(statusClanarineFilter);
            }
            else
            {
                ds = _adminUpravljanjeKorisnicima.DajSveKorisnikeSaStatusomClanarine();
            }

            AdminViewModel model = new AdminViewModel
            {
                Korisnici = new List<KorisnikAdminViewModel>(),
                PrezimePretraga = prezimePretraga,
                StatusClanarineFilter = statusClanarineFilter
            };

            foreach (DataRow red in ds.Tables[0].Rows)
            {
                model.Korisnici.Add(new KorisnikAdminViewModel
                {
                    KorisnikID = Convert.ToInt32(red["KorisnikID"]),
                    Ime = red["Ime"].ToString(),
                    Prezime = red["Prezime"].ToString(),
                    Email = red["Email"].ToString(),
                    StatusClanarine = red["StatusClanarine"].ToString()
                });
            }



            return View(model);
        }

        public IActionResult ProfilKorisnika(int korisnikId)
        {
			DataSet ds = _adminUpravljanjeKorisnicima.DajProfilKorisnikaZaAdmina(korisnikId);

			if (ds == null || ds.Tables[0].Rows.Count == 0)
			{
				return RedirectToAction("Index");
			}


			DataRow red = ds.Tables[0].Rows[0];

            int mesec = DateTime.Today.Month;
            int godina = DateTime.Today.Year;

            int procenatPopusta =_obracunClanarine.IzracunajPopust(korisnikId, mesec, godina);

            decimal cenaSaPopustom =_obracunClanarine.IzracunajClanarinu(korisnikId, mesec, godina);

            KorisnikProfilAdminViewModel model = new KorisnikProfilAdminViewModel
            {
                KorisnikID = Convert.ToInt32(red["KorisnikID"]),
                Ime = red["Ime"].ToString(),
                Prezime = red["Prezime"].ToString(),
                Email = red["Email"].ToString(),
                BrojTelefona = red["BrojTelefona"].ToString(),
                Pol = red["Pol"].ToString(),
                StatusClanarine = red["StatusClanarine"].ToString(),

                DatumRodjenja = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumRodjenja"])),
                DatumAktivacije = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumAktivacije"])),
                DatumIsteka = DateOnly.FromDateTime(Convert.ToDateTime(red["DatumIsteka"])),
                Cena = cenaSaPopustom,
                Popust =procenatPopusta,
                ZahtevZaProduzenje = Convert.ToBoolean(red["ZahtevZaProduzenje"])
            };


                return View(model);


		}

        [HttpPost]
        public IActionResult OdbijZahtev(int korisnikID)
        {
            string rezultat = _slanjeZahtevaZaProduzenjeClanarine.OdbijZahtev(korisnikID);

            switch (rezultat)
            {
                case ("NemaClanarinu"):
                    TempData["PorukaAdmin"] = "Korisnik nema članarinu.";
                    break;

                case ("NemaZahteva"):
                    TempData["PorukaAdmin"] = "Korisnik nema poslat zahtev.";
                    break;
                case ("Uspesno"):
                    TempData["PorukaAdmin"] = "Zahtev za produženje članarine je odbijen.";
                    break;
                default:
                    TempData["PorukaAdmin"] = "Došlo je do greške.";
                        break;
            }

            return RedirectToAction("ProfilKorisnika", new {korisnikId = korisnikID});
        }

        [HttpPost]
        public IActionResult PotvrdiUplatu(int korisnikID)
        {
            string rezultat = _slanjeZahtevaZaProduzenjeClanarine.PotvrdiUplatu(korisnikID);

            switch (rezultat)
            {
                case ("NemaClanarinu"):
                    TempData["Poruka"] = "Korisnik nema članarinu.";
                    break;

                case ("NemaZahteva"):
                    TempData["Poruka"] = "Korisnik nema poslat zahtev.";
                    break;
                case ("Uspesno"):
                    TempData["Poruka"] = "Uplata je uspešno potvrđena.";
                    break;
                default:
                    TempData["Poruka"] = "Došlo je do greške.";
                    break;
            }

            return RedirectToAction("ProfilKorisnika", new { korisnikId = korisnikID });
        }

        public IActionResult Odjava()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Prijava");
        }

    }
}
