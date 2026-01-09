using Microsoft.AspNetCore.Http;

namespace AlzaProductApi.Web.Extensions;

public static class HttpResponsePaginationExtensions
{
	public static void AddPaginationHeaders(
		this HttpResponse response,
		int totalCount,
		int page,
		int pageSize,
		Func<int, string> buildPageUrl)
	{
		response.Headers["X-Total-Count"] = totalCount.ToString();
		response.Headers["X-Page"] = page.ToString();
		response.Headers["X-Page-Size"] = pageSize.ToString();

		var lastPage = (int)Math.Ceiling(totalCount / (double)pageSize);
		if (lastPage < 1) lastPage = 1;

		var links = new List<string>();

		if (page > 1)
			links.Add($"<{buildPageUrl(page - 1)}>; rel=\"prev\"");
		if (page < lastPage)
			links.Add($"<{buildPageUrl(page + 1)}>; rel=\"next\"");

		links.Add($"<{buildPageUrl(1)}>; rel=\"first\"");
		links.Add($"<{buildPageUrl(lastPage)}>; rel=\"last\"");

		response.Headers["Link"] = string.Join(", ", links);
	}
}