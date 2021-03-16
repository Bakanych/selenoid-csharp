using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Web;

namespace Test
{
    public class Fixture
    {
        private WebApplication app;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.app = WebApplicationFactory.GetInstance();
        }

        [Test]
        public void GetConsoleErrorLogs()
        {
            this.app.GoTo("");
            Console.WriteLine(this.app.WebDriver.Title);
            Assert.That(this.app.WebDriver.Title, Is.EqualTo("Test Title"));
            var logs = this.app.GetLogs();
            Assert.That(logs, Has.Exactly(1).Items.Matches<LogEntry>(x=>x.Level == LogLevel.Severe));

            
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this.app.Dispose();
            
        }
    }
}