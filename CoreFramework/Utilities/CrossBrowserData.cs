using CoreFramework.Configs;
using CoreFramework.DriverCore;
using NUnit.Framework;
using System.Collections;

namespace CoreFramework.Utilities
{
    public class CrossBrowserData : WebDriverAction
    {
        
        public static IEnumerable FullConfigurations
        {

            get
            {
                yield return new TestFixtureData("chrome");
                yield return new TestFixtureData("firefox");
            }
        }

        public static IEnumerable SimpleConfigurations
        {
            get
            {
                yield return new TestFixtureData("chrome");
            }
        }

        public static IEnumerable GetBrowserConfig
        {
            get
            {  
                string browserDefault = ConfigManager.GetConfig<BrowserConfig>("BrowserConfig").BrowserConfig_;
                var browserType = TestContext.Parameters.Get("browserType", browserDefault);
                if (browserType.Equals("simple"))
                {
                    return SimpleConfigurations;
                }
                else
                {
                    return FullConfigurations;
                }
            }
        }       
    }
}
