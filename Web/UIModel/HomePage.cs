using HtmlElements.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Web.UIModel;

public class HomePage : BasePage
{
    [FindsBy(How = How.Id, Using = "content")]
    public HtmlElement Content;
    
    public HomePage(ISearchContext webDriverOrWrapper) : base(webDriverOrWrapper)
    {
    }
}