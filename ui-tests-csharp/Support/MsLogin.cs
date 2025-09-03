using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace UiTests.Support;

public static class MsLogin
{
	public static async Task LoginIfNeededAsync(IPage page, string appBaseUrl, string user, string pass)
	{
		// If not on AAD and no AAD widgets visible, nothing to do.
		if (!IsAad(page.Url))
		{
			var looksLikeAad =
					 await page.GetByRole(AriaRole.Heading, new() { NameRegex = new Regex("^Sign in$", RegexOptions.IgnoreCase) }).IsVisibleSafe(600)
				|| await page.Locator("input[name='loginfmt'], #i0118, input[name='passwd']").First.IsVisibleSafe(600);

			if (!looksLikeAad) return;
		}

		// 1. Pick an account tile if "Pick an account" shows
		if (await ClickFirstVisibleAsync(page,
					page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex(Regex.Escape(user), RegexOptions.IgnoreCase) }),
					page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex(Regex.Escape(user), RegexOptions.IgnoreCase) }),
					page.Locator("div[role='button']", new() { HasTextRegex = new Regex(Regex.Escape(user), RegexOptions.IgnoreCase) }),
					page.Locator("div[role='link']", new() { HasTextRegex = new Regex(Regex.Escape(user), RegexOptions.IgnoreCase) })
				))
		{
			await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
			await WaitAnyVisible(page, 10_000, "input[name='loginfmt']", "input[name='passwd']", "#i0118");
		}

		// 2. Email page
		var email = page.Locator("input[name='loginfmt']");
		if (await email.IsVisibleSafe())
		{
			await email.FillAsync(user);

			var nextBtn = page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex("^Next$", RegexOptions.IgnoreCase) })
												.Or(page.Locator("input[type='submit'][value='Next']"));

			if (await nextBtn.First.IsVisibleSafe()) await nextBtn.First.ClickAsync();

			await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
			await WaitAnyVisible(page, 15_000, "input[name='passwd']", "#i0118");
		}

		// 3. Password page
		var pwd = page.Locator("input[name='passwd'], #i0118");
		if (await pwd.IsVisibleSafe())
		{
			await pwd.FillAsync(pass);

			var signIn = page.Locator("#idSIButton9")
											 .Or(page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex("^Sign in$", RegexOptions.IgnoreCase) }))
											 .Or(page.Locator("input[type='submit'][value='Sign in']"));

			if (await signIn.First.IsVisibleSafe()) await signIn.First.ClickAsync();

			// Wait for either "Stay signed in?" or redirect back to the app origin
			var origin = new Uri(appBaseUrl).GetLeftPart(UriPartial.Authority);
			await WaitStayYesOrApp(page, origin, 30_000);
		}

		//4. "Stay signed in?" (optional)
		var yes = page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex("^Yes$", RegexOptions.IgnoreCase) })
									.Or(page.Locator("input[type='submit'][value='Yes']"));

		if (await yes.First.IsVisibleSafe(3000))
		{
			await yes.First.ClickAsync();
			await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
		}

		// 5. Ensure we're back on the app origin and network is idle
		var appOrigin = new Uri(appBaseUrl).GetLeftPart(UriPartial.Authority);
		await page.WaitForURLAsync(new Regex("^" + Regex.Escape(appOrigin) + ".*"), new() { Timeout = 120_000 });
		await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
	}

	private static bool IsAad(string url) => url.Contains("login.microsoftonline.com", StringComparison.OrdinalIgnoreCase);

	private static async Task<bool> ClickFirstVisibleAsync(IPage page, params ILocator[] locators)
	{
		foreach (var loc in locators)
		{
			if (loc == null) continue;
			if (await loc.First.IsVisibleSafe(1200))
			{
				await loc.First.ClickAsync();
				return true;
			}
		}
		return false;
	}

	private static async Task<bool> WaitAnyVisible(IPage page, int timeoutMs, params string[] selectors)
	{
		var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);
		while (DateTime.UtcNow < deadline)
		{
			foreach (var sel in selectors)
			{
				if (string.IsNullOrWhiteSpace(sel)) continue;
				if (await page.Locator(sel).First.IsVisibleSafe(200)) return true;
			}
			await Task.Delay(200);
		}
		return false;
	}

	private static async Task WaitStayYesOrApp(IPage page, string appOrigin, int timeoutMs)
	{
		var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);
		while (DateTime.UtcNow < deadline)
		{
			if (page.Url.StartsWith(appOrigin, StringComparison.OrdinalIgnoreCase)) return;

			var stayYes = page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex("^Yes$", RegexOptions.IgnoreCase) })
												.Or(page.Locator("input[type='submit'][value='Yes']"));

			if (await stayYes.First.IsVisibleSafe(250)) return;

			await Task.Delay(250);
		}
	}

	// Safe IsVisible that never throws
	private static async Task<bool> IsVisibleSafe(this ILocator locator, int timeoutMs = 1500)
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
