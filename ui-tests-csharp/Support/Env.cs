using DotNetEnv;
using System;

namespace UiTests.Support;

public static class Env
{
	public static string Get(string name)
	{
		DotNetEnv.Env.TraversePath().Load();

		return Environment.GetEnvironmentVariable(name) ?? throw new InvalidOperationException($"Environment variable '{name}' is required.");
	}


	public static string BaseNoSlash(string name) => Get(name).TrimEnd('/');
}
