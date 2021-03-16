using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Core
{
    public static class Config
    {
        private const string EnvPrefix = "UITests_Config_";
        private static readonly JObject jsonConfig;
        private static readonly IEnumerable<string> envKeys;

        static Config()
        {
            jsonConfig =
                JObject.Parse(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
            envKeys = Environment.GetEnvironmentVariables().Keys.Cast<string>();
        }

        public static BrowserType Browser => GetSetting<BrowserType>(nameof(Browser));

        public static string BaseUrl
        {
            get
            {
                var value = GetSetting<string>(nameof(BaseUrl));
                return (value.StartsWith("http")) ? value : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value);
            }
        }

        public static bool Selenoid => GetSetting<bool>(nameof(Selenoid));
        public static string SelenoidHubUrl => GetSetting<string>(nameof(SelenoidHubUrl));

        private static T GetSetting<T>(string testKey, string appKey = null)
        {
            var envKey =
                envKeys.FirstOrDefault(x =>
                    string.Equals(x, $"{EnvPrefix}{testKey}", StringComparison.InvariantCultureIgnoreCase));
            if (envKey != null)
                return (T) TypeDescriptor.GetConverter(typeof(T))
                    .ConvertFromString(Environment.GetEnvironmentVariable(envKey));

            var configVar = jsonConfig.GetValue(testKey, StringComparison.InvariantCultureIgnoreCase);
            var configValue = (configVar != null) ? configVar.ToObject<T>() : default;

            return configValue;
        }
    }
}