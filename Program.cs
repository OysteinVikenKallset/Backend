// Inkluderer nødvendige navnerom for webapplikasjonen, databasetilgang, og Swagger/OpenAPI-dokumentasjon.
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Users.Models;

// Initialiserer en WebApplication-builder med argumentene som er gitt til programmet.
var builder = WebApplication.CreateBuilder(args);

// Legger til tjenester i DI-containeren (Dependency Injection).

// Legger til MVC-kontrollere til applikasjonen.
builder.Services.AddControllers();

// Konfigurerer CORS-policy som tillater forespørsler fra alle kilder, med alle metoder og headere.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Konfigurerer Entity Framework Core til å bruke SQLite som databaselagring, basert på en tilkoblingsstreng.
builder.Services.AddDbContext<UsersContext>(opt =>
     opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Legger til støtte for Swagger, som automatisk genererer API-dokumentasjon basert på dine kontrollere og ruter.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Bygger applikasjonen.
var app = builder.Build();

// Konfigurerer HTTP-forespørselsrørledningen.

// Aktiverer Swagger UI og Swagger-dokumentasjon i utviklingsmiljøet.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aktiverer ruting, som er nødvendig for å definere API-endepunkter.
app.UseRouting();

// Aktiverer den tidligere definerte CORS-policyen "AllowAll".
app.UseCors("AllowAll");

// Aktiverer autorisasjonsmidler, selv om det ikke er definert noen spesifikk autorisasjonslogikk her.
app.UseAuthorization();

// Tvinger applikasjonen til å bruke HTTPS for alle forespørsler.
app.UseHttpsRedirection();

// En ekstra kall til UseAuthorization uten spesifikk grunn. Dette kan være en feil eller unødvendig.
app.UseAuthorization();

// Mapper kontrollere til ruter, slik at de kan håndtere forespørsler.
app.MapControllers();

// Starter applikasjonen og begynner å lytte på innkommende forespørsler.
app.Run();