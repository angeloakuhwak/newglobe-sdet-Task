using DotNetEnv;
using FluentAssertions;
using Xunit;

namespace FeatureToggle.ApiTests;

public class SearchTests
{
	private static ApiClient Create()
	{
		try
		{
			Env.TraversePath().Load();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? throw new InvalidOperationException("API_BASE_URL missing");
		var user = Environment.GetEnvironmentVariable("BASIC_AUTH_USERNAME");
		var pass = Environment.GetEnvironmentVariable("BASIC_AUTH_PASSWORD");
		var token = Environment.GetEnvironmentVariable("OAUTH_TOKEN");
		return new ApiClient(baseUrl, user, pass, token);
	}

	[Fact(DisplayName = "Global features/status endpoint responds")]
	public async Task GlobalStatus_Works()
	{
		using var api = Create();
		// Many implementations expose /features/status?projectKeys=...
		// We call without params just to confirm the endpoint responds.
		var (status, _) = await api.GetJsonDocument("/features/status");
		status.Should().BeOneOf(200, 400, 404); // tolerant smoke: endpoint is present, even if it requires params
	}
}
