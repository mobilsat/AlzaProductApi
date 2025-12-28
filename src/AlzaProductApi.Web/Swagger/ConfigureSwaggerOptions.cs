using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
//using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AlzaProductApi.Web.Swagger;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
	: IConfigureOptions<SwaggerGenOptions>
{
	public void Configure(SwaggerGenOptions options)
	{
		foreach (var description in provider.ApiVersionDescriptions)
		{
			options.SwaggerDoc(description.GroupName, new OpenApiInfo
			{
				Title = "AlzaProductApi",
				Version = description.ApiVersion.ToString()
			});
		}
	}
}