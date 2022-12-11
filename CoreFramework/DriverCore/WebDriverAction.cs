using CoreFramework.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Framework.DriverCore
{
    public class WebDriverAction
    {
        public IWebDriver driver;
        private WebDriverWait _explicitWait;


        public WebDriverAction(IWebDriver? driver)
        {
            this.driver = driver;
        }

        public Boolean IsElementDisplayed(string locator)
        {
            try
            {
                WaitToBeVisible(locator);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public bool IsTextElementCorrect(string locator, string key)
        {
            string text = GetTextElement(locator);
            if (text == key)
            {
                TestContext.WriteLine("Text Element " + locator + " is displayed correctly");
                return true;
            }
            else
            {
                TestContext.WriteLine("Text Element " + locator + " is displayed incorrectly");
                return false;
            }
        }

        public void GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
            HtmlReport.Pass("Go to url: " + url);
        }
        public void Quit()
        {
            driver.Quit();
            HtmlReport.Pass("Quit the browser");
        }


        public void Back()
        {
            try
            {
                driver.Navigate().Back();
                HtmlReport.Pass("Get back passed");
            }
            catch (Exception ex)
            {
                HtmlReport.Fail("Get back failed", TakeScreenshot());
                throw ex;
            }
        }

        public void Forward()
        {
            try
            {
                driver.Navigate().Forward();
                HtmlReport.Pass("Get forward passed");
            }
            catch (Exception ex)
            {
                HtmlReport.Fail("Get forward failed", TakeScreenshot());
                throw ex;
            }

        }

        public string GetUrl()
        {
            string url = driver.Url.ToString();
            return url;
        }

        public string GetTitle()
        {
            string title = driver.Title;
            return title;

        }
        public string TakeScreenshot()
        {
            string path = HtmlReportDirectory.SCREENSHOT_PATH + ("/screenshot_" + DateTime.Now.ToString("yyyyMMddHHmmss")) + ".png";
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            return path;
        }

        public IWebElement HighLightElement(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].style.border = '2px solid red'", element);
            return element;
        }

        public IWebElement WaitToBeVisible(string locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                TestContext.WriteLine("Element " + locator + " is visible");
                return element;
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Element " + locator + " is not visible");
                throw ex;
            }
            
        }
        public IWebElement WaitToBeClickable(string locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                return element;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IWebElement IsElementExist(string locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(locator)));
                return element;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IWebElement FindElementByXpath(string locator)
        {
            IWebElement e = driver.FindElement(By.XPath(locator));
            HighLightElement(e);          
            return e;
        }

        public IWebElement FindElementByCssSlector(string locator)
        {
            IWebElement e = driver.FindElement(By.CssSelector(locator));
            HighLightElement(e);
            return e;
        }

        public IList<IWebElement> CreatList (string locator)
        {
            IList<IWebElement> elements = driver.FindElements(By.XPath(locator));
            return elements;
        }


        public IWebElement ScrollToElement(string locator)
        {
            IWebElement e = FindElementByXpath(locator);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].scrollIntoView();", e);
            return e;
        }


        public void Click(IWebElement e)
        {
            try
            {
                HighLightElement(e);
                e.Click();
                TestContext.WriteLine("Click into element " + e.ToString() + " passed");
                HtmlReport.Pass("Click into element " + e.ToString() + " passed");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Click into element " + e.ToString() + " failed");
                HtmlReport.Fail("Click into element " + e.ToString() + " failed", TakeScreenshot());
                throw ex;
            }
        }

        public void Click(string locator)
        {
            try
            {
                FindElementByXpath(locator).Click();
                HtmlReport.Pass("Click into element " + locator + " passed");

            }
            catch (Exception ex)
            {
                HtmlReport.Fail("Click into element " + locator + " failed", TakeScreenshot());
                throw ex;
            }
        }

        public void SendKeys_(IWebElement e, string key)
        {
            try
            {
                e.SendKeys(key)
;
                HtmlReport.Pass("SendKey into element " + e.ToString() + " passed");
            }
            catch (Exception ex)
            {
                HtmlReport.Fail("SendKey into element " + e.ToString() + " failed", TakeScreenshot());
                throw ex;
            }
        }

        public void Remove(string locator)
        {
            try
            {
                Click(locator);
                FindElementByXpath(locator).SendKeys(Keys.Control + "a");
                FindElementByXpath(locator).SendKeys(Keys.Delete);
                HtmlReport.Pass("Clear element text " + locator + " passed");
            }
            catch (Exception ex)
            {
                HtmlReport.Fail("Clear element text " + locator + " failed", TakeScreenshot());
                throw ex;
            }
        }

        public void SendKeys_(string locator, string key)
        {
            try
            {
                Remove(locator);
                FindElementByXpath(locator).SendKeys(key);
                HtmlReport.Pass("SendKey into element " + locator + " passed");
            }
            catch (Exception ex)
            {
                HtmlReport.Fail("SendKey into element " + locator + " failed", TakeScreenshot());
                throw ex;
            }
        }

        public string GetTextElement(string locator)
        {
            try
            {
                IWebElement e = FindElementByXpath(locator);
                string text = e.Text;
                return text;
                HtmlReport.Pass("Get Text Element " + locator + " passed");
            }
            catch (Exception ex)
            {
                HtmlReport.Fail("Get Text Element " + locator + " failed", TakeScreenshot());
                throw ex;
            }
        }       
    }
}
