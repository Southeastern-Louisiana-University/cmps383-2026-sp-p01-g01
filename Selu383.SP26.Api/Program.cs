using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api.Data;
using Selu383.SP26.Api.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        await db.Database.MigrateAsync(); 
    }
using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        if (!db.Locations.Any())
        {   
            db.Locations.AddRange(
            new Location { Name = "Downtown Coffee Co.", Address = "101 Main St", TableCount = 12 },
            new Location { Name = "Riverwalk Café", Address = "220 Riverside Dr", TableCount = 8 },
            new Location { Name = "Campus Brew House", Address = "35 University Ave", TableCount = 16 },
            new Location { Name = "Oak Street Roasters", Address = "89 Oak St", TableCount = 10 },
            new Location { Name = "Sunrise Espresso Bar", Address = "450 Sunrise Blvd", TableCount = 14 },
            new Location { Name = "Lakeside Coffee Lounge", Address = "77 Lakeview Rd", TableCount = 18 },
            new Location { Name = "Midtown Mocha House", Address = "742 Midtown Ave", TableCount = 15 },
            new Location { Name = "The Corner Bean", Address = "12 Third Ave", TableCount = 9 },
            new Location { Name = "Harbor Brew Café", Address = "300 Harbor Blvd", TableCount = 20 },
            new Location { Name = "Garden Patio Coffee", Address = "122 Blossom Ct", TableCount = 13 });

            await db.SaveChangesAsync();
        }
    }
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//see: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0
// Hi 383 - this is added so we can test our web project automatically
public partial class Program { }
