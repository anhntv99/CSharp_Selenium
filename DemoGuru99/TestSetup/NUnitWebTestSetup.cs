using CoreFramework.Configs;
using CoreFramework.DriverCore;
using CoreFramework.NUnitTestSetup;
using NUnit.Framework;

namespace DemoGuru99.TestSetup;

public class NUnitWebTestSetup : NUnitTestSetup
{
    public NUnitWebTestSetup(string browser) : base(browser)
    {
    }

    [SetUp]
    public void WebTestSetUp()
    {
        AppConfig appConfig = ConfigManager.GetConfig<AppConfig>("Application");

        /// Initialize header pages
      
        DriverBaseAction = new WebDriverAction();
        DriverBaseAction.GoToUrl(appConfig.BaseUrl);
              
       
    }
    [TearDown]
    public void WebTestTearDown()
    {
       
    }
}
