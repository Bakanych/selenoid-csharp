using System;
using Core;
using NUnit.Framework;
using Web;
using Web.UIModel;

namespace Test;

public abstract class BaseFixture
{
    protected WebApplication App;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        App = WebApplicationFactory.GetInstance();
        Console.WriteLine($"Environment name: ${Config.EnvironmentName}");
    }

    [SetUp]
    public void BeforeTest()
    {
        App.NavigateTo<HomePage>(Config.TestData.Get<string>("testPage"));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        App.Dispose();
    }
}