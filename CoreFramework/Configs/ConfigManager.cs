using CoreFramework.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using ServiceStack;

namespace CoreFramework.Configs
{
    public class ConfigManager
    {
        private static IConfigurationRoot _configurationRoot;

        public static void Configure()
        {
            _configurationRoot = new ConfigurationBuilder()
               .SetBasePath(FilePaths.GetCurrentDirectoryPath())
               .AddJsonFile($"appsettings.json", true)
               .AddEnvironmentVariables()
               .Build();            
        }

        public static T GetConfig<T>(string key) where T : class
        {
            if(_configurationRoot == null)
            {
                Configure();
            }    
            return _configurationRoot.GetSection(key).Get<T>();
        }

        public static T GetValue<T>(string key) where T : class
        {
            return _configurationRoot.GetValue<T>(key);
        }
    }
}
