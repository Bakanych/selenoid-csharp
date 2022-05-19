using System;
using System.Collections.Generic;
using System.Drawing;
using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;

namespace Web;

// https://github.com/SeleniumHQ/selenium/issues/8229
internal class RemoteWebDriverWithLogs : RemoteWebDriver, ISupportsLogs
{
    public RemoteWebDriverWithLogs(DriverOptions options) : base(options)
    {
    }

    public RemoteWebDriverWithLogs(ICapabilities desiredCapabilities) : base(desiredCapabilities)
    {
    }

    public RemoteWebDriverWithLogs(Uri remoteAddress, DriverOptions options) : base(remoteAddress, options)
    {
    }

    public RemoteWebDriverWithLogs(Uri remoteAddress, ICapabilities desiredCapabilities) : base(remoteAddress,
        desiredCapabilities)
    {
    }

    public RemoteWebDriverWithLogs(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout) :
        base(remoteAddress, desiredCapabilities, commandTimeout)
    {
    }

    public RemoteWebDriverWithLogs(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities) : base(
        commandExecutor, desiredCapabilities)
    {
    }
}

public sealed class WebDriverFactory
{
    public static IWebDriver GetWebDriver(bool withEvents)
    {
        Func<IWebDriver> webDriverFunc;
        DriverOptions options;

        switch (Config.TestSettings.Browser)
        {
            case BrowserType.Chrome:
                options = new ChromeOptions();
                // ((ChromeOptions) options).AddArgument("no-sandbox");
                // ((ChromeOptions) options).AddArgument("headless");
                options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                webDriverFunc = () =>
                {
                    var driver = new ChromeDriver((ChromeOptions) options);
                    return driver;
                };
                break;

            case BrowserType.Firefox:
                options = new FirefoxOptions {AcceptInsecureCertificates = true};
                options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                webDriverFunc = () => new FirefoxDriver((FirefoxOptions) options);
                break;

            default:
                throw new Exception($"Browser {Config.TestSettings.Browser} is not supported");
        }

        if (Config.TestSettings.IsSelenoid)
        {
            options.AddAdditionalOption("selenoid:options", new Dictionary<string, object>
            {
                ["enableLog"] = true,
                ["enableVnc"] = false,
                ["enableVideo"] = false
            });
            options.AddAdditionalOption("env", new Dictionary<string, object>
            {
                ["VERBOSE"] = true
            });
            webDriverFunc = () => new RemoteWebDriverWithLogs(new Uri(Config.TestSettings.SelenoidHubUrl), options);
        }

        var webDriver = webDriverFunc.Invoke();
        webDriver.Manage().Window.Size = new Size(1600, 800);

        return withEvents ? GetEventFiringWebDriver(webDriver) : webDriver;
    }

    private static EventFiringWebDriver GetEventFiringWebDriver(IWebDriver driver)
    {
        var eventDriver = new EventFiringWebDriver(driver);
        eventDriver.ElementClicking += EventDriver_ElementClicking;
        eventDriver.ElementValueChanging += EventDriver_ElementValueChanging;
        return eventDriver;
    }

    private static void EventDriver_ElementValueChanging(object sender, WebElementValueEventArgs e)
    {
        e.Element.Highlight();
    }

    private static void EventDriver_ElementClicking(object sender, WebElementEventArgs e)
    {
        e.Element.Highlight();
    }
}