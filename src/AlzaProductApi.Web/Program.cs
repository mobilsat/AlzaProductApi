using AlzaProductApi.Core.Interfaces;
using AlzaProductApi.Infrastructure.Data;
using AlzaProductApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddOpenApi(); //  TODO ... now OK, Swagger later

// --------------------
// Build
// --------------------

var app = builder.Build();

// --------------------
// HTTP pipeline
// --------------------

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();