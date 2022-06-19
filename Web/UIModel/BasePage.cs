using System;
using System.Collections.Generic;
using System.Linq;
using HtmlElements.Elements;
using HtmlElements.Extensions;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Web.UIModel;

public abstract class BasePage : HtmlPage
{
    private readonly TimeSpan pollingInterval = TimeSpan.FromMilliseconds(100);
    private readonly TimeSpan timeout = TimeSpan.FromSeconds(10);

    [FindsBy(How = How.Id, Using = "loading")]
    private IList<IWebElement> loaders;

    protected BasePage(ISearchContext webDriverOrWrapper) : base(webDriverOrWrapper)
    {
    }

    public void WaitUntilLoaded()
    {
        loaders.WaitUntil(list => !list.Any(x => x.Displayed), timeout, pollingInterval);
    }
}