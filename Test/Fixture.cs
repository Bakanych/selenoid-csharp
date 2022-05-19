using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Web;

namespace Test;

public class Fixture
{
    private WebApplication app;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        app = WebApplicationFactory.GetInstance();
    }

    [Test]
    public void GetConsoleErrorLogs()
    {
        app.GoTo("");
        Console.WriteLine(app.WebDriver.Title);
        Assert.That(app.WebDriver.Title, Is.EqualTo("Test Title"));
        var logs = app.GetLogs();
        Assert.That(logs, Has.Some.Matches<LogEntry>(x => x.Level == LogLevel.Severe));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        app.Dispose();
    }
}