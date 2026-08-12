using Microsoft.Extensions.FileProviders;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "XML")),
    RequestPath = "/XML"
});

app.MapGet("/api/popust/parametri", () =>
{
    string putanjaDoXML = Path.Combine(builder.Environment.ContentRootPath, "XML", "PraviloZaPopust.xml");

    DataSet skupPodataka = new DataSet();

    skupPodataka.ReadXml(putanjaDoXML);

    var tabela = skupPodataka.Tables[0];

    var rezultat = tabela.Rows.Cast<DataRow>()
    .Select(red => new
    {
        MinBrojRealizacija = int.Parse(red["MinBrojRealizacija"].ToString()),
        ProcenatPopusta = int.Parse(red["ProcenatPopusta"].ToString())
    })
    .ToList();

    if (rezultat.Count == null)
    {
        return Results.NotFound("Parametri popusta nisu pronadjeni.");
    }

    return Results.Ok(rezultat);
});

app.MapGet("/api/clanarina/cena", () =>
{
    string putanjaDoXML = Path.Combine(builder.Environment.ContentRootPath, "XML", "CenaClanarine.xml");

    DataSet skupPodataka = new DataSet();

    skupPodataka.ReadXml(putanjaDoXML);

    var tabela = skupPodataka.Tables[0];

    var rezultat = tabela.Rows.Cast<DataRow>()
    .Select(red => new
    {
        OsnovnaCena = decimal.Parse(red["OsnovnaCena"].ToString())

    })
    .FirstOrDefault();

    if (rezultat == null)
    {
        return Results.NotFound("Cena clanarine nije pronadjena.");
    }

    return Results.Ok(rezultat);
});

app.Run();
