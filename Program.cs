using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ParkingSolution.Context;
using ParkingSolution.Entities;
using ParkingSolution.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddScoped<IParkingRepository, ParkingRepository>();
services.AddDbContext<ParkingSolutionContext>
(o => o.UseInMemoryDatabase("IMDB"));

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Swagger services
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Parking API", Version = "v1" });
});

// Add controllers
services.AddControllers();
services.AddRazorPages();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(static c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parking API");
    });

}
app.MapRazorPages();
app.UseRouting();
app.MapControllers();
AddParkings(app);
app.Run();
static void AddParkings(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<ParkingSolutionContext>();

    var parkingFirst = new Parking
    {
        Id = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
        ParkingSpot = "N21",
        ReservationDateFrom = DateTime.ParseExact("3/21/2023", "M/dd/yyyy", CultureInfo.InvariantCulture),
        ReservationDateTo = DateTime.ParseExact("3/25/2023", "M/dd/yyyy", CultureInfo.InvariantCulture),
        CreationDate = DateTime.ParseExact("3/21/2023", "M/dd/yyyy", CultureInfo.InvariantCulture)

    };

    var parkingSecond = new Parking
    {
        Id = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
        ParkingSpot = "N23",
        ReservationDateFrom = DateTime.ParseExact("9/11/2023", "M/dd/yyyy", CultureInfo.InvariantCulture),
        ReservationDateTo = DateTime.ParseExact("3/22/2023", "M/dd/yyyy", CultureInfo.InvariantCulture),
        CreationDate = DateTime.ParseExact("9/11/2023", "M/dd/yyyy", CultureInfo.InvariantCulture)
    };

    var parkingThree = new Parking
    {
        Id = new Guid("24810dfc-2d94-4cc7-aab5-cdf98b83f0c9"),
        ParkingSpot = "N29",
        ReservationDateFrom = DateTime.ParseExact("7/11/2023", "M/dd/yyyy", CultureInfo.InvariantCulture),
        ReservationDateTo = DateTime.ParseExact("8/23/2023", "M/dd/yyyy", CultureInfo.InvariantCulture),
        CreationDate = DateTime.ParseExact("7/11/2023", "M/dd/yyyy", CultureInfo.InvariantCulture)
    };

    db.Parkings.Add(parkingFirst);
    db.Parkings.Add(parkingSecond);
    db.Parkings.Add(parkingThree);

    db.SaveChanges();
}

