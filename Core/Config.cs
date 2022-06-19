using System;
using Microsoft.Extensions.Configuration;

namespace Core;

public class TestSettings
{
    public BrowserType Browser { get; set; }
    public string BaseUrl { get; set; }
    public bool IsSelenoid { get; set; }
    public string SelenoidHubUrl { get; set; }
}

public static class Config
{
    private static readonly IConfigurationSection TestDataSection;

    static Config()
    {
        var env = Environment.GetEnvironmentVariable(Const.TestEnvironmentVariableName) ?? "local";
        ConfigurationBuilder builder = new();

        // remove existing sources
        builder.Sources.Clear();
        // set base path to bin
        builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

        var configurationRoot = builder
            .AddJsonFile("config.json", false, true)
            .AddJsonFile($"config.{env}.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        TestSettings = configurationRoot.GetSection(nameof(TestSettings)).Get<TestSettings>();
        TestDataSection = configurationRoot.GetSection(nameof(TestData));
    }

    public static string EnvironmentName => Environment.GetEnvironmentVariable(Const.TestEnvironmentVariableName);
    public static TestSettings TestSettings { get; }

    public static class TestData
    {
        public static T Get<T>(string key)
        {
            return TestDataSection.GetSection(key.Replace(".", ":")).Get<T>();
        }
    }
}