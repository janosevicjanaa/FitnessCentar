using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.ViewModels;
using KlasePoslovneLogike;
using KlasePodataka.EntitetKlase;

namespace PrezentacioniSloj.Controllers
{
    public class PrijavaController: Controller
    {

        private readonly PrijavaKorisnikaKlasa _prijavaKorisnika;

        public PrijavaController(PrijavaKorisnikaKlasa prijavaKorisnika)
        {
            _prijavaKorisnika = prijavaKorisnika;
        }
    
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(PrijavaViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            KorisnikKlasa korisnik = _prijavaKorisnika.PrijaviKorisnika(model.Email, model.Lozinka);

            if(korisnik == null)
            {
                ModelState.AddModelError("", "Pogrešan email ili lozinka.");
                return View(model);
            }

            HttpContext.Session.SetInt32("KorisnikID", korisnik.KorisnikID);

            if(korisnik.Uloga == "Administrator")
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Korisnik");
        }
    }
}
