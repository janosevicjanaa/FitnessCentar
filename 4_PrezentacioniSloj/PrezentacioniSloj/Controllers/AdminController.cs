using KlasePodataka.EntitetKlase;
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

            List<ClanarinaKlasa> clanarine;

            if (!string.IsNullOrWhiteSpace(prezimePretraga))
            {
                clanarine = _adminUpravljanjeKorisnicima.DajKorisnikePoPrezimenu(prezimePretraga);
            }
            else if (!string.IsNullOrWhiteSpace(statusClanarineFilter))
            {
                clanarine = _adminUpravljanjeKorisnicima.DajKorisnikePoStatusuClanarine(statusClanarineFilter);
            }
            else
            {
                clanarine = _adminUpravljanjeKorisnicima.DajSveKorisnikeSaStatusomClanarine();
            }

            AdminViewModel model = new AdminViewModel
            {
                Korisnici = new List<KorisnikAdminViewModel>(),
                PrezimePretraga = prezimePretraga,
                StatusClanarineFilter = statusClanarineFilter
            };

            foreach (ClanarinaKlasa clanarina in clanarine)
            {
                model.Korisnici.Add(new KorisnikAdminViewModel
                {
                    KorisnikID = clanarina.Korisnik.KorisnikID,
                    Ime = clanarina.Korisnik.Ime,
                    Prezime = clanarina.Korisnik.Prezime,
                    Email = clanarina.Korisnik.Email,
                    StatusClanarine = clanarina.StatusClanarine
                });
            }

            return View(model);
        }

        public IActionResult ProfilKorisnika(int korisnikId)
        {
            ClanarinaKlasa korisnik =_adminUpravljanjeKorisnicima.DajProfilKorisnikaZaAdmina(korisnikId);

            if (korisnik == null)
            {
                return RedirectToAction("Index");
            }

            int mesec = DateTime.Today.Month;
            int godina = DateTime.Today.Year;

            int procenatPopusta =_obracunClanarine.IzracunajPopust(korisnikId, mesec, godina);

            decimal cenaSaPopustom =_obracunClanarine.IzracunajClanarinu(korisnikId, mesec, godina);

            KorisnikProfilAdminViewModel model = new KorisnikProfilAdminViewModel
            {
                KorisnikID = korisnik.Korisnik.KorisnikID,
                Ime = korisnik.Korisnik.Ime,
                Prezime = korisnik.Korisnik.Prezime,
                Email = korisnik.Korisnik.Email,
                BrojTelefona = korisnik.Korisnik.BrojTelefona,
                Pol = korisnik.Korisnik.Pol,

                StatusClanarine = korisnik.StatusClanarine,

                DatumRodjenja = korisnik.Korisnik.DatumRodjenja,

                DatumAktivacije = korisnik.DatumAktivacije,
                DatumIsteka = korisnik.DatumIsteka,

                Cena = cenaSaPopustom,
                Popust = procenatPopusta,

                ZahtevZaProduzenje = korisnik.ZahtevZaProduzenje
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
