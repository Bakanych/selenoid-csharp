using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using Core;
using OpenQA.Selenium;
using Web.UI;
using LogEntry = OpenQA.Selenium.LogEntry;

namespace Web
{
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
        public int Id { get; }

        public WebApplication(int id)
        {
            this.Id = id;
        }

        public Page GoTo(string path)
        {
            if (this.WebDriver == null)
            {
                this.WebDriver = WebDriverFactory.GetWebDriver(false);
            }

            this.WebDriver.Url = Path.Combine(Config.BaseUrl, path);
            return new Page();
        }

     

        public ReadOnlyCollection<LogEntry> GetLogs()
        {
            return this.WebDriver?.Manage().Logs.GetLog(LogType.Browser);
        }

        public void Dispose()
        {
            this.WebDriver?.Quit();
        }
    }
}