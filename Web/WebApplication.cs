using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using Core;
using OpenQA.Selenium;
using Web.UI;

namespace Web;

public static class WebApplicationFactory
{
    private static readonly ThreadLocal<WebApplication> threadedClient = new(() =>
        new WebApplication(id++), true);

    private static int id = 1;

    public static void DisposeAll()
    {
        threadedClient.Values.ToList().ForEach(x => x.Dispose());
    }

    public static WebApplication GetInstance()
    {
        return threadedClient.Value;
    }
}

public class WebApplication : IDisposable
{
    public IWebDriver WebDriver;

    public WebApplication(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public void Dispose()
    {
        WebDriver?.Quit();
    }

    public Page NavigateTo(string path)
    {
        WebDriver ??= WebDriverFactory.GetWebDriver(false);
        var baseUrl = Config.TestSettings.BaseUrl.StartsWith("http")
            ? Config.TestSettings.BaseUrl
            : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Config.TestSettings.BaseUrl);
        WebDriver.Url = Path.Combine(baseUrl, path);
        return new Page();
    }

    public ReadOnlyCollection<LogEntry> GetLogs()
    {
        return WebDriver != null && WebDriver.Manage().Logs.AvailableLogTypes.Contains(LogType.Browser)
            ? WebDriver?.Manage().Logs.GetLog(LogType.Browser)
            : new ReadOnlyCollection<LogEntry>(new List<LogEntry>());
    }
}