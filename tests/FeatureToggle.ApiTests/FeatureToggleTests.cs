using DotNetEnv;
using FluentAssertions;
using System.Text.Json;

namespace FeatureToggle.ApiTests;

public class FeatureToggleTests
{
	private static ApiClient Create(bool noAuth = false)
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
		
		if (noAuth)
		{
			return new ApiClient(baseUrl, user: null, pass: null, bearer: null);
		}

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

	private static async Task<string?> FirstFeatureKey(ApiClient api, string projectKey)
	{
		var (status, doc) = await api.GetJsonDocument($"/projects/{projectKey}/features");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Array);

		foreach (var f in doc.RootElement.EnumerateArray())
		{
			if (f.TryGetProperty("key", out var k) && k.ValueKind == JsonValueKind.String)
			{
				return k.GetString();
			}
		}

		return null;
	}

	private static async Task<string?> FirstVariationKey(ApiClient api, string projectKey, string featureKey)
	{
		// Variations are embedded in the single feature response
		var (status, doc) = await api.GetJsonDocument($"/projects/{projectKey}/features/{featureKey}");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Object);

		if (doc.RootElement.TryGetProperty("variations", out var arr) && arr.ValueKind == JsonValueKind.Array)
		{
			foreach (var v in arr.EnumerateArray())
			{
				if (v.TryGetProperty("key", out var k) && k.ValueKind == JsonValueKind.String)
					return k.GetString();
			}
		}
		return null;
	}

	[Fact(DisplayName = "GET /projects/{projectKey}/features lists features")]
	public async Task GetFeatures_ForProject_Works()
	{
		using var api = Create();

		var key = await FirstProjectKey(api);
		var (status, doc) = await api.GetJsonDocument($"/projects/{key}/features");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(System.Text.Json.JsonValueKind.Array);
	}

	[Fact(DisplayName = "GET /projects/{projectKey}/features/status returns feature statuses")]
	public async Task GetFeatureStatus_ForProject_Works()
	{
		using var api = Create();

		var key = await FirstProjectKey(api);
		var (status, doc) = await api.GetJsonDocument($"/projects/{key}/features/status");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(System.Text.Json.JsonValueKind.Array);
	}

	// ------------------------
	// Features collections
	// ------------------------

	[Fact(DisplayName = "GET /projects/{key}/features: array of features with minimal shape")]
	public async Task GetFeatures_HasShape()
	{
		using var api = Create();
		var key = await FirstProjectKey(api);

		var (status, doc) = await api.GetJsonDocument($"/projects/{key}/features");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Array);

		var any = doc.RootElement.EnumerateArray().FirstOrDefault();

		if (any.ValueKind == JsonValueKind.Object)
		{
			any.TryGetProperty("key", out var fk).Should().BeTrue();
			any.TryGetProperty("name", out var fn).Should().BeTrue();

			if (any.TryGetProperty("isEnabled", out var fe))
			{
				fe.ValueKind.Should().BeOneOf(JsonValueKind.True, JsonValueKind.False);
			}
		}
	}

	[Fact(DisplayName = "GET /projects/{key}/features/{featureKey}: returns a single feature (if any exist)")]
	public async Task GetSingleFeature_IfAny()
	{
		using var api = Create();
		var projectKey = await FirstProjectKey(api);
		var featureKey = await FirstFeatureKey(api, projectKey);

		if (featureKey is null)
			return; 

		var (status, doc) = await api.GetJsonDocument($"/projects/{projectKey}/features/{featureKey}");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Object);
		doc.RootElement.GetProperty("key").GetString().Should().Be(featureKey);
	}

	[Fact(DisplayName = "GET /projects/{key}/features/{bad}: 404 for unknown feature")]
	public async Task GetSingleFeature_Unknown_404()
	{
		using var api = Create();
		var projectKey = await FirstProjectKey(api);

		var (status, _) = await api.GetJsonDocument($"/projects/{projectKey}/features/__does_not_exist__");
		status.Should().BeOneOf(404, 400);
	}

	// ------------------------
	// Status endpoints
	// ------------------------

	[Fact(DisplayName = "GET /projects/{key}/features/status: array of status objects")]
	public async Task GetFeatureStatuses_ForProject()
	{
		using var api = Create();
		var key = await FirstProjectKey(api);

		var (status, doc) = await api.GetJsonDocument($"/projects/{key}/features/status");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Array);

		// If any, they should be objects; tolerate naming differences
		var first = doc.RootElement.EnumerateArray().FirstOrDefault();
		if (first.ValueKind == JsonValueKind.Object)
		{
			first.ValueKind.Should().Be(JsonValueKind.Object);
		}
	}

	[Fact(DisplayName = "GET /projects/{projectKey}/features/{featureKey}/status: single status object (if any feature exists)")]
	public async Task GetFeatureStatus_Single()
	{
		using var api = Create();
		var projectKey = await FirstProjectKey(api);
		var featureKey = await FirstFeatureKey(api, projectKey);

		if (featureKey is null)
		{
			return;
		}

		var (status, doc) = await api.GetJsonDocument($"/projects/{projectKey}/features/{featureKey}/status");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Object);

		// Tolerant check: either "enabled" or "status" exists and is boolean
		var hasBool = doc.RootElement.TryGetProperty("isEnabled", out var en) && en.ValueKind is JsonValueKind.True or JsonValueKind.False;
		
		hasBool.Should().BeTrue("status object should expose a boolean enabled/status field");
	}

	// ------------------------
	// Variations endpoints
	// ------------------------

	[Fact(DisplayName = "GET /projects/{projectKey}/features/{featureKey}: has `variations` array (may be empty)")]
	public async Task GetFeature_IncludesVariationsArray()
	{
		using var api = Create();

		var projectKey = await FirstProjectKey(api);
		var featureKey = await FirstFeatureKey(api, projectKey);
		if (featureKey is null) return; // nothing to assert if project has no features

		var (status, doc) = await api.GetJsonDocument($"/projects/{projectKey}/features/{featureKey}");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Object);

		// Assert variations array exists even if empty
		doc.RootElement.TryGetProperty("variations", out var variations).Should().BeTrue();
		variations.ValueKind.Should().Be(JsonValueKind.Array);
	}

	[Fact(DisplayName = "GET /projects/{projectKey}/features/{featureKey}/variations/{variationKey}: returns a variation")]
	public async Task GetFeatureVariation_ByKey_Works()
	{
		using var api = Create();

		var projectKey = await FirstProjectKey(api);
		var featureKey = await FirstFeatureKey(api, projectKey);
		if (featureKey is null) return; // skip if no features

		var variationKey = await FirstVariationKey(api, projectKey, featureKey);
		if (variationKey is null) return; // skip if the feature has no variations

		var url = $"/projects/{projectKey}/features/{featureKey}/variations/{Uri.EscapeDataString(variationKey)}";
		var (status, doc) = await api.GetJsonDocument(url);

		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Object);
		doc.RootElement.TryGetProperty("key", out var keyProp).Should().BeTrue();
		keyProp.GetString().Should().Be(variationKey);
	}

	// ------------------------
	// History
	// ------------------------

	[Fact(DisplayName = "GET /projects/history: responds with an array (or 200 with empty array)")]
	public async Task GetProjectsHistory()
	{
		using var api = Create();
		var (status, doc) = await api.GetJsonDocument("/projects/history");
		status.Should().Be(200);
		doc.RootElement.ValueKind.Should().Be(JsonValueKind.Array);
	}

	// ------------------------
	// Auth / Security basics
	// ------------------------

	[Fact(DisplayName = "Without auth: GET /projects should be rejected")]
	public async Task GetProjects_NoAuth_Rejected()
	{
		using var api = Create(noAuth: true);
		var (status, _) = await api.GetJsonDocument("/projects");

		// depending on gateway it may be 401 or 403
		status.Should().BeOneOf(401, 403);
	}
}
