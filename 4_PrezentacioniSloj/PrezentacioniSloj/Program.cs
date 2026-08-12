using KlasePodataka.InterfejsKlase;
using KlasePodataka.KontekstKlasa;
using KlasePodataka.Repository;
using KlasePoslovneLogike;
using KlasePoslovneLogike.PomocneKlase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<PrijavaKorisnikaKlasa>();
builder.Services.AddScoped<UpravljanjeProfilomKorisnikaKlasa>();
builder.Services.AddScoped<RegistracijaKorisnikaKlasa>();
builder.Services.AddScoped<AdminUpravljanjeKorisnicimaKlasa>();
builder.Services.AddScoped<SlanjeZahtevaZaProduzenjeClanarineKlasa>();
builder.Services.AddScoped<UpravljanjeRealizacijamaVezbiKlasa>();
builder.Services.AddScoped<UpravljanjeTipovimaVezbiKlasa>();
builder.Services.AddScoped<OgranicenjeZaDodavanjeRealizacijeKlasa>();
builder.Services.AddScoped<ObracunClanarineKlasa>();
builder.Services.AddDbContext<FitnessCentarKontekst>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FitnessCentar")));


builder.Services.AddScoped<ISPKorisnikDB>(sp => new SPKorisnikDBKlasa(builder.Configuration.GetConnectionString("FitnessCentar")));
builder.Services.AddScoped<ISPClanarinaDB>(sp => new SPClanarinaDBKlasa(builder.Configuration.GetConnectionString("FitnessCentar")));
builder.Services.AddScoped<ISPRealizacijaVezbeDB>(sp => new SPRealizacijaVezbeDBKlasa(builder.Configuration.GetConnectionString("FitnessCentar")));
builder.Services.AddScoped<ITipVezbeEF, TipVezbeEFKlasa>();
builder.Services.AddScoped<RESTServisClanarineKlasa>(sp => new RESTServisClanarineKlasa(builder.Configuration["RestServis:Url"]));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Prijava}/{action=Index}/{id?}");

app.Run();
