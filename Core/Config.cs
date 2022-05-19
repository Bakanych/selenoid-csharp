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
    static Config()
    {
        ConfigurationBuilder builder = new();

        // remove existing sources
        builder.Sources.Clear();
        // set base path to bin
        builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

        var configurationRoot = builder
            .AddJsonFile("config.json", false, true)
            .AddEnvironmentVariables()
            .Build();

        TestSettings = configurationRoot.Get<TestSettings>();
    }

    public static TestSettings TestSettings { get; }
}