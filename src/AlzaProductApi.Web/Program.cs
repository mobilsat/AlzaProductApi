using AlzaProductApi.Core.Interfaces;
using AlzaProductApi.Infrastructure.Data;
using AlzaProductApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer; // because of IApiVersionDescriptionProvider
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using AlzaProductApi.Web.Swagger;
using AlzaProductApi.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();


builder.Services
	.AddApiVersioning(options =>
	{
		options.DefaultApiVersion = new ApiVersion(1, 0);
		options.AssumeDefaultVersionWhenUnspecified = true;
		options.ReportApiVersions = true;
		options.ApiVersionReader = new UrlSegmentApiVersionReader();
	})
	.AddMvc()
	.AddApiExplorer(options =>
	{
		options.GroupNameFormat = "'v'VVV";
		options.SubstituteApiVersionInUrl = true;
	});


var useMock = builder.Configuration.GetValue<bool>("UseMockRepository");

if (useMock)
{
	builder.Services.AddSingleton<IProductRepository>(_ =>
		new InMemoryProductRepository(ProductSeed.Get()));
}
else
{
	builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

	builder.Services.AddScoped<IProductRepository, ProductRepository>();
}


builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		foreach (var desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
		{
			//options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
			options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"v{desc.ApiVersion}");
		}
	});
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program { }
