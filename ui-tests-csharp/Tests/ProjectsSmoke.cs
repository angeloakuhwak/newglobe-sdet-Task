using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using UiTests.Support;

namespace UiTests.Tests;

[TestFixture, Parallelizable(ParallelScope.Self)]
public class ProjectsSmoke : PageTest
{
	public override BrowserNewContextOptions ContextOptions()
		=> new() { StorageStatePath = PlaywrightLoginFixture.StoragePath };

	[Test]
	[Retry(5)] // Small insurance vs. transient network/AAD hiccups
	public async Task LoginAndSeeProjects()
	{
		var baseUrl = Env.Get("UI_BASE_URL");
		var baseNoSlash = Env.BaseNoSlash("UI_BASE_URL");
		var user = Env.Get("UI_USERNAME");
		var pass = Env.Get("UI_PASSWORD");

		// Generous defaults for this flow
		Page.SetDefaultTimeout(30_000);
		Page.SetDefaultNavigationTimeout(120_000);

		// Go straight to /projects (app will bounce to AAD if needed)
		await Page.GotoAsync($"{baseNoSlash}/projects", new() { WaitUntil = WaitUntilState.DOMContentLoaded });

		// If AAD shows, complete login (storage state might be stale)
		if (IsAad(Page.Url) || await LooksLikeAadAsync(Page))
		{
			await MsLogin.LoginIfNeededAsync(Page, baseUrl, user, pass);
		}

		// --- Stabilization loop: keep nudging the SPA until “Projects” is truly loaded ---
		var deadline = DateTime.UtcNow.AddSeconds(90);
		bool seen = false;

		while (DateTime.UtcNow < deadline)
		{
			// If bounced back to AAD mid-flight, finish login again
			if (IsAad(Page.Url) || await LooksLikeAadAsync(Page))
			{
				await MsLogin.LoginIfNeededAsync(Page, baseUrl, user, pass);
			}

			// Make sure we’re on the app origin and at /projects
			var appOrigin = new Uri(baseUrl).GetLeftPart(UriPartial.Authority);
			if (!Page.Url.StartsWith(appOrigin, StringComparison.OrdinalIgnoreCase) ||
					!Page.Url.Contains("/projects", StringComparison.OrdinalIgnoreCase))
			{
				await Page.GotoAsync($"{baseNoSlash}/projects", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
			}

			await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
			await Task.Delay(1000); // small delay

			// Accept any of these as proof the Projects page is ready
			if (await AnyVisibleAsync(
						Page.GetByRole(AriaRole.Heading, new() { NameRegex = new Regex("projects", RegexOptions.IgnoreCase) }),
						Page.Locator("h1:has-text('Projects')"),
						Page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex("^History$", RegexOptions.IgnoreCase) }),
						Page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex("^Add$", RegexOptions.IgnoreCase) }),
						Page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex("^Logout$", RegexOptions.IgnoreCase) }),
						Page.Locator("section:has-text('Projects')") // extra fallback
					))
			{
				seen = true;
				break;
			}

			// Kick the SPA if it stalled
			await Page.ReloadAsync(new() { WaitUntil = WaitUntilState.NetworkIdle });
		}

		await Page.ScreenshotAsync(new() { Path = "artifacts/final-before-assert.png", FullPage = true });
		Assert.That(seen, Is.True, "Expected Projects dashboard after login.");

		// Look for a visible Project Key that is not hidden
		await Expect(Page.Locator(":text('PROJECT KEY'):visible").First).ToBeVisibleAsync(new() { Timeout = 5000 });
	}

	private static bool IsAad(string url) => url.Contains("login.microsoftonline.com", StringComparison.OrdinalIgnoreCase);

	private static async Task<bool> LooksLikeAadAsync(IPage page)
	{
		return await page.GetByRole(AriaRole.Heading, new() { NameRegex = new Regex("^Sign in$", RegexOptions.IgnoreCase) }).IsVisibleSafe(600)
				|| await page.Locator("input[name='loginfmt'], input[name='passwd'], #i0118").First.IsVisibleSafe(600);
	}

	private static async Task<bool> AnyVisibleAsync(params ILocator[] locators)
	{
		foreach (var l in locators)
		{
			if (await l.First.IsVisibleSafe(600))
			{
				return true;
			}
		}

		return false;
	}
}

static class LocatorExt
{
	public static async Task<bool> IsVisibleSafe(this ILocator locator, int timeoutMs = 1500)
	{
		try
		{
			return await locator.IsVisibleAsync(new() { Timeout = timeoutMs });
		}
		catch
		{
			return false;
		}
	}
}