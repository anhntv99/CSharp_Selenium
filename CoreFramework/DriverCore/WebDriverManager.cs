using CoreFramework.Configs;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace CoreFramework.DriverCore;

public class WebDriverManager
{
    private static AsyncLocal<IWebDriver> driver = new AsyncLocal<IWebDriver>();
    public static IWebDriver? CreateLocalDriver(string browser, int? screenWidth = null, int? screenHeight = null)
    {
        IWebDriver? LocalDriver = null;
        switch(browser)
        {
            case "chrome":              
                LocalDriver = new ChromeDriver();
                break;
            case "firefox":
                LocalDriver = new FirefoxDriver();
                break;
            case "safari":
                LocalDriver = new SafariDriver();
                break;
        }
        LocalDriver.Manage().Window.Maximize();
        LocalDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        return LocalDriver;
    }
    public static RemoteWebDriver? CreateRemoteDriver(string browser)
    {
        Uri DOCKER_GRID_HUB_URI = new Uri("http://localhost:4444/wd/hub");
        RemoteWebDriver? RemoteDriver = null;
        switch (browser)
        {
            case "chrome":
                ChromeOptions chromeCapability = new ChromeOptions
                {
                    BrowserVersion = "",
                    PlatformName = "LINUX",
                };
                chromeCapability.AddArgument("--start-maximized");
                RemoteDriver = new RemoteWebDriver(DOCKER_GRID_HUB_URI, chromeCapability.ToCapabilities());
                break;
            case "firefox":
                FirefoxOptions firefoxCapability = new FirefoxOptions
                {
                    BrowserVersion = "",
                    PlatformName = "LINUX",
                };
                firefoxCapability.AddArgument("--start-maximized");
                RemoteDriver = new RemoteWebDriver(DOCKER_GRID_HUB_URI, firefoxCapability.ToCapabilities());
                break;
            default:
                throw new ArgumentException($"{browser} is not supported remotely.");
        }
        return RemoteDriver;
    }
    public static void InitDriver(string browser, int? width = null, int? height = null)
    {
        FrameworkConfiguration frameworkConfiguration = ConfigManager.GetConfig<FrameworkConfiguration>("Framework");
        TestContext.WriteLine(frameworkConfiguration.ExecuteLocation);
        if (frameworkConfiguration.ExecuteLocation.Equals("local"))
        {
            
            IWebDriver newLocalDriver = CreateLocalDriver(browser, width, height);

            if (newLocalDriver == null)
                throw new Exception($"{browser} browser is not supported locally");
            driver.Value = newLocalDriver;
        }
        else if (frameworkConfiguration.ExecuteLocation.Equals("remote"))
        {
            RemoteWebDriver newRemoteDriver = CreateRemoteDriver(browser);
            if (newRemoteDriver == null)
                throw new Exception($"{browser} browser is not supported locally");
            driver.Value = newRemoteDriver;
        }
      

    }
    public static IWebDriver GetCurrentDriver()
    {
        return driver.Value;    
    }
    public static void CloseDriver()
    {
        if (driver.Value != null)
        {
            driver.Value.Quit();
            driver.Value.Dispose();
        }
    }
}

