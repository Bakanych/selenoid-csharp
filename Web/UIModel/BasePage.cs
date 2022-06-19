using System.Collections.Generic;
using System.Linq;
using HtmlElements.Elements;
using HtmlElements.Extensions;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Web.UIModel;

public abstract class BasePage : HtmlPage
{
    [FindsBy(How = How.Id, Using = "loading")]
    private IList<IWebElement> loaders;
    
    protected BasePage(ISearchContext webDriverOrWrapper) : base(webDriverOrWrapper)
    {
    }

    public void WaitUntilLoaded()
    {
        loaders.WaitUntil(list => !list.Any(x => x.Displayed));
    }
}