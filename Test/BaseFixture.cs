using Core;
using NUnit.Framework;
using Web;
using Web.UIModel;

namespace Test;

public abstract class BaseFixture
{
    protected WebApplication App;



    [SetUp]
    public void BeforeTest()
    {
        App = WebApplicationFactory.GetInstance();
        App.NavigateTo<HomePage>(Config.TestData.Get<string>("testPage"));
    }
}