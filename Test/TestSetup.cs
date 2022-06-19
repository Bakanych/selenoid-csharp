using System;
using Core;
using NUnit.Framework;
using Web;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace Test;

[SetUpFixture]
internal class TestSetup
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Console.WriteLine($"Environment name: ${Config.EnvironmentName}");
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        WebApplicationFactory.DisposeAll();
    }
    
}