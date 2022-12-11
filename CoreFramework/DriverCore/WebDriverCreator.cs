using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using System.Drawing;



namespace Framework.DriverCore
{
    public class WebDriverCreator
    {
        public static WebDriver CreateLocalDriver(string Browser, int ScreenWidth, int ScreenHight)
        {
            IWebDriver Driver = null;
            if (Browser.SequenceEqual("firefox"))
            {
                //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                Driver = new FirefoxDriver();
            }

            else if (Browser.SequenceEqual("chrome"))
            {
                //new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                Driver = new ChromeDriver();
            }
            else if (Browser.SequenceEqual("safari"))
            {
                //new WebDriverManager.DriverManager().SetUpDriver(new SafariConfig());
                Driver = new SafariDriver();
            }
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Size = new Size(ScreenWidth, ScreenHight);
            return (WebDriver)Driver;
        }
    }   
}
