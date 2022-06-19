using System;
using Microsoft.Extensions.Configuration;

namespace Core;

public static class TestData
{
    private static readonly IConfigurationRoot DataRoot;

    static TestData()
    {
        Environment.SetEnvironmentVariable(Const.TestEnvironmentVariableName, "stage");

        var env = Environment.GetEnvironmentVariable(Const.TestEnvironmentVariableName) ?? "local";
        ConfigurationBuilder builder = new();
        builder.Sources.Clear();
        builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

        DataRoot = builder
            .AddJsonFile($"{Const.TestDataDefaultFileName}.json", false, true)
            .AddJsonFile($"{Const.TestDataDefaultFileName}.{env}.json", true, true)
            .AddEnvironmentVariables(Const.TestDataEnvPrefix)
            .Build();
    }


    public static string EnvironmentName => Environment.GetEnvironmentVariable(Const.TestEnvironmentVariableName);

    public static T Get<T>(string key)
    {
        return DataRoot.GetSection(key.Replace(".", ":")).Get<T>();
    }
}