using KlasePodataka.EntitetKlase;
using KlasePoslovneLogike;
using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.ViewModels;

namespace PrezentacioniSloj.Controllers
{
	public class RegistracijaController : Controller
	{
		private readonly RegistracijaKorisnikaKlasa _registracijaKorisnika;

		public RegistracijaController(RegistracijaKorisnikaKlasa registracijaKorisnika)
		{
			_registracijaKorisnika = registracijaKorisnika;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(RegistracijaViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			KorisnikKlasa korisnik = new KorisnikKlasa
			{
				Ime = model.Ime,
				Prezime = model.Prezime,
				Email = model.Email,
				Lozinka = model.Lozinka,
				BrojTelefona = model.BrojTelefona,
				DatumRodjenja = model.DatumRodjenja,
				Pol = model.Pol
			};

			int korisnikID = _registracijaKorisnika.RegistrujKorisnika(korisnik);

			if (korisnikID == 0)
			{
				ModelState.AddModelError("", "Registracija nije uspela");
				return View(model);
			}

			HttpContext.Session.SetInt32("KorisnikID", korisnikID);

			return RedirectToAction("Index", "Korisnik");
		}
	}
}
