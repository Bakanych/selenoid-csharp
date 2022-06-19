using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using Core;
using HtmlElements;
using OpenQA.Selenium;
using Web.UIModel;

namespace Web;

public static class WebApplicationFactory
{
    private static readonly ThreadLocal<WebApplication> threadedClient = new(() =>
        new WebApplication(), true);

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
    private PageObjectFactory pof = new();

    public void Dispose()
    {
        WebDriver?.Quit();
    }

    public T NavigateTo<T>(string path) where T:BasePage
    {
        WebDriver ??= WebDriverFactory.GetWebDriver(false);
        var baseUrl = Config.TestSettings.BaseUrl.StartsWith("http")
            ? Config.TestSettings.BaseUrl
            : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Config.TestSettings.BaseUrl);
        WebDriver.Url = Path.Combine(baseUrl, path);
        var page = pof.Create<T>(WebDriver);
        page!.WaitUntilLoaded();
        return page;
    }

    public ReadOnlyCollection<LogEntry> GetLogs()
    {
        return WebDriver != null && WebDriver.Manage().Logs.AvailableLogTypes.Contains(LogType.Browser)
            ? WebDriver?.Manage().Logs.GetLog(LogType.Browser)
            : new ReadOnlyCollection<LogEntry>(new List<LogEntry>());
    }
}