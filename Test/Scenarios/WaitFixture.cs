using System.Diagnostics;
using NUnit.Framework;
using Web.UIModel;

namespace Test.Scenarios;

public class WaitFixture: BaseFixture
{
    [Test]
    public void ItShouldWaitForPageToLoad()
    {
        var timer = new Stopwatch();
        timer.Start();
        var page = App.NavigateTo<HomePage>("slow.html");
        var pageLoadTime = decimal.Parse(page.Content.TextContent); 
        timer.Stop();
        Assert.That(timer.ElapsedMilliseconds - pageLoadTime, Is.LessThan(200));
    }
    
}