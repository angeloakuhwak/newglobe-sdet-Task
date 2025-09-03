using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using UiTests.Support;

namespace UiTests.Tests;

[SetUpFixture]
public class PlaywrightLoginFixture
{
	public static string StoragePath => Path.Combine(TestContext.CurrentContext.WorkDirectory, ".auth", "auth.json");

	[OneTimeSetUp]
	public async Task GlobalSetup()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(StoragePath)!);

		var baseUrl = Env.Get("UI_BASE_URL");
		var baseNoSlash = Env.BaseNoSlash("UI_BASE_URL");
		var user = Env.Get("UI_USERNAME");
		var pass = Env.Get("UI_PASSWORD");
		var headless = Environment.GetEnvironmentVariable("HEADFUL") == "1";

		using var pw = await Playwright.CreateAsync();

		await using var browser = await pw.Chromium.LaunchAsync(new() { Headless = headless, SlowMo = 50 });
		var context = await browser.NewContextAsync();
		var options = new TracingStartOptions
		{
			Screenshots = true,
			Snapshots = true,
			Sources = true
		};
		await context.Tracing.StartAsync(options);
		var page = await context.NewPageAsync();

		// Go straight to /projects; app will bounce to AAD if needed
		await page.GotoAsync($"{baseNoSlash}/projects", new() { WaitUntil = WaitUntilState.DOMContentLoaded });

		// Allow a brief window for the SPA/MSAL redirect to AAD to happen
		var appOriginEsc = Regex.Escape(new System.Uri(baseUrl).GetLeftPart(System.UriPartial.Authority));
		try
		{
			await page.WaitForURLAsync(
				new Regex($"(login\\.microsoftonline\\.com|{appOriginEsc})", RegexOptions.IgnoreCase),
				new() { Timeout = 10_000 }
			);
		}
		catch
		{
			/* best effort */
		}

		// Call the login helper unconditionally; it will early exit if not on AAD
		await MsLogin.LoginIfNeededAsync(page, baseUrl, user, pass);

		// Sanity: confirm Projects is really there
		var ok =
			await page.GetByRole(AriaRole.Heading, new() { NameRegex = new Regex("projects", RegexOptions.IgnoreCase) }).IsVisibleAsync().CatchFalse()
			|| await page.Locator("h1:has-text(\"Projects\")").IsVisibleAsync().CatchFalse()
			|| await page.Locator("text=PROJECT KEY").First.IsVisibleAsync().CatchFalse();

		if (!ok)
			await page.GotoAsync($"{baseNoSlash}/projects", new() { WaitUntil = WaitUntilState.NetworkIdle });

		await context.StorageStateAsync(new() { Path = StoragePath });

		Directory.CreateDirectory("artifacts");
		await context.Tracing.StopAsync(new() { Path = "artifacts/ui-trace.zip" });
		await context.CloseAsync();
	}
}

public static class BoolTasks
{
	public static async Task<bool> CatchFalse(this Task<bool> t)
	{
		try
		{
			return await t;
		}
		catch
		{
			return false;
		}
	}
}