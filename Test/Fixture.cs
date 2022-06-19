using Core;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Test;

public class Fixture : BaseFixture
{
    [Test]
    public void GetBrowserTitle()
    {
        Assert.That(App.WebDriver.Title, Is.EqualTo(TestData.Get<string>("pageTitle")));
    }

    [Test]
    [IgnoreInBrowser(BrowserType.Firefox)]
    public void GetConsoleErrorLogs()
    {
        var errorMessage = TestData.Get<string>("errorMessage");
        var logs = App.GetLogs();
        Assert.That(logs,
            Has.Some.Matches<LogEntry>(x => x.Level == LogLevel.Severe && x.Message.Contains(errorMessage)));
    }
}