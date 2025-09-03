using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FeatureToggle.ApiTests;

public sealed class ApiClient : IDisposable
{
	private readonly HttpClient _http;
	private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };
	private string BaseUrl { get; }

	public ApiClient(string baseUrl, string? user, string? pass, string? bearer = null)
	{
		BaseUrl = baseUrl.TrimEnd('/');

		if (string.IsNullOrWhiteSpace(baseUrl))
		{
			throw new ArgumentException("baseUrl required", nameof(baseUrl));
		}

		_http = new HttpClient { BaseAddress = new Uri(baseUrl, UriKind.Absolute) };

		// Required by API
		_http.DefaultRequestHeaders.Accept.Clear();
		_http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		_http.DefaultRequestHeaders.UserAgent.Clear();
		_http.DefaultRequestHeaders.Add("User-Agent", "NewGlobeSdetTests/1.0 (Windows)");

		// Auth: Bearer preferred if present; else Basic
		if (!string.IsNullOrWhiteSpace(bearer))
		{
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
		}
		else if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pass))
		{
			var raw = $"{user}:{pass}";
			var b64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(raw));
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", b64);
		}
	}

	private string Url(string path) => $"{BaseUrl}/{path.TrimStart('/')}";

	private async Task<HttpResponseMessage> Get(string path)
	{
		// Add per-request correlation header
		using var req = new HttpRequestMessage(HttpMethod.Get, Url(path));
		req.Headers.TryAddWithoutValidation("X-Request-ID", Guid.NewGuid().ToString());

		// Accept is already on defaults, but harmless if also present here
		req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		return await _http.SendAsync(req);
	}

	public async Task<T?> GetJson<T>(string path)
	{
		var res = await Get(path);
		res.EnsureSuccessStatusCode();
		await using var s = await res.Content.ReadAsStreamAsync();
		return await JsonSerializer.DeserializeAsync<T>(s, _json);
	}

	//public async Task<(int Status, JsonDocument Body)> GetJsonDocument(string path)
	//{
	//	var res = await Get(path);
	//	var status = (int)res.StatusCode;
	//	var stream = await res.Content.ReadAsStreamAsync();
	//	var doc = await JsonDocument.ParseAsync(stream);
	//	return (status, doc);
	//}

	public async Task<(int Status, JsonDocument Body)> GetJsonDocument(string path)
	{
		using var res = await Get(path); // make sure Get(...) does NOT call EnsureSuccessStatusCode
		var status = (int)res.StatusCode;

		if (res.Content == null)
			return (status, JsonDocument.Parse("null"));

		var contentType = res.Content.Headers.ContentType?.MediaType;
		var text = await res.Content.ReadAsStringAsync();

		// Empty body ? return JSON null
		if (string.IsNullOrWhiteSpace(text))
			return (status, JsonDocument.Parse("null"));

		// Parse only if JSON or looks like it
		if (!string.IsNullOrEmpty(contentType) &&
		    contentType.IndexOf("json", StringComparison.OrdinalIgnoreCase) >= 0)
		{
			return (status, JsonDocument.Parse(text));
		}

		var trimmed = text.TrimStart();
		if (trimmed.StartsWith("{") || trimmed.StartsWith("["))
			return (status, JsonDocument.Parse(text));

		// Non-JSON body (e.g., HTML) ? wrap safely so callers can still inspect it if needed
		var wrapped = System.Text.Json.JsonSerializer.Serialize(new
		{
			nonJson = trimmed.Length > 1000 ? trimmed[..1000] + "…" : trimmed
		});
		return (status, JsonDocument.Parse(wrapped));
	}


	public void Dispose() => _http.Dispose();
}
