using NUnit.Framework;
using Web;

namespace Test;

public abstract class BaseFixture
{
    protected WebApplication App;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        App = WebApplicationFactory.GetInstance();
    }

    [SetUp]
    public void BeforeTest()
    {
        App.NavigateTo("");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        App.Dispose();
    }
}