using OpenQA.Selenium;

namespace Framework.DriverCore
{
    public class WebDriverManager
    {

        private static AsyncLocal<IWebDriver> driver = new AsyncLocal<IWebDriver>();
        public static void InitDriver(string Browser, int Width, int Hight)
        {
            IWebDriver newDriver = null;
            newDriver = WebDriverCreator.CreateLocalDriver(Browser, Width, Hight);

            if (driver == null)
                throw new Exception($"{Browser} browser is not supported");
            driver.Value = newDriver;
        }

        public static IWebDriver GetCurrentDriver()
        {
            return driver.Value;
        }


        public static void CloseDriver()
        {
            if(driver.Value != null)
            {
                driver.Value.Quit();
                driver.Value.Dispose();
            }
        }
    }
}
