using System;
using Core;
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
        Console.WriteLine($"Environment name: ${TestData.EnvironmentName}");
    }

    [SetUp]
    public void BeforeTest()
    {
        App.NavigateTo(TestData.Get<string>("homePage"));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        App.Dispose();
    }
}