using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using AeroVeloz.Infraestructure.Persistence.context;
=======
//using AeroVeloz.Infraestructure.Persistence.context;
>>>>>>> app-web

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

<<<<<<< HEAD
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AeroVelozContext>(options => 
    options.UseSqlServer(connectionString));
=======
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AeroVelozDbContext>(options =>
//    options.UseSqlServer(connectionString));
>>>>>>> app-web

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
