using DotNetEnv;
using FluentAssertions;
using System.Text.Json;

namespace FeatureToggle.ApiTests;

public class ProjectTests
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

	private static async Task<string> FirstProjectKey(ApiClient api)
	{
		var projects = await api.GetJson<List<Dictionary<string, object>>>("/projects");
		projects.Should().NotBeNull();
		projects!.Count.Should().BeGreaterThan(0, "at least one project should exist for smoke tests");
		return projects[0]["key"]!.ToString()!;
	}

	// ------------------------
	// /projects & /projects/{key}
	// ------------------------

	[Fact(DisplayName = "GET /projects: each item has minimally key + name")]
	public async Task GetProjects_HasShape()
	{
		using var api = Create();

		var (status, doc) = await api.GetJsonDocument("/projects");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Array);

		var first = doc.RootElement.EnumerateArray().First();
		first.TryGetProperty("key", out var keyProp).Should().BeTrue();
		keyProp.ValueKind.Should().Be(JsonValueKind.String);
		first.TryGetProperty("name", out var nameProp).Should().BeTrue();
		nameProp.ValueKind.Should().Be(JsonValueKind.String);
	}

	[Fact(DisplayName = "GET /projects/{key}: returns a single project object")]
	public async Task GetProject_ByKey()
	{
		using var api = Create();
		var key = await FirstProjectKey(api);

		var (status, doc) = await api.GetJsonDocument($"/projects/{key}");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Object);
		doc.RootElement.GetProperty("key").GetString().Should().Be(key);
	}

	[Fact(DisplayName = "GET /projects/{badKey}: returns 404")]
	public async Task GetProject_Unknown_404()
	{
		using var api = Create();
		var (status, _) = await api.GetJsonDocument($"/projects/__does_not_exist__");
		status.Should().BeOneOf(404, 400);
	}
}
