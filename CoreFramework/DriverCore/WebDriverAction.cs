using CoreFramework.Configs;
using CoreFramework.Reporter;
using CoreFramework.Utilities;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Globalization;
using DriverManager = CoreFramework.DriverCore.WebDriverManager;

namespace CoreFramework.DriverCore;
public class WebDriverAction
{
    public IWebDriver driver;
    public IJavaScriptExecutor Javascript { get; set; }
    private WebDriverWait _explicitWait;
    private Actions _actions;
    private int _timeWait = 60;
    private string browser;
    protected string browserName;
    protected int browserWidth;
    protected int browserHeight;
    public static string MergedPath;
    public static string BaselinePath;
    public static string ActualPath;
    public static double SimilarRate;
    private ImageProcessor imageProcessor;

    public WebDriverAction()
    {
        driver = DriverManager.GetCurrentDriver();
        _actions = new Actions(driver);
        _explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(_timeWait));
        Javascript = (IJavaScriptExecutor)driver;
        browserWidth = driver.Manage().Window.Size.Width;
        browserHeight = driver.Manage().Window.Size.Height;
        browserName = driver.GetType().Name;

    }

    public WebDriverAction(string browser)
    {
        this.browser = browser;
    }

    public void GoToUrl(string url)
    {
        driver.Navigate().GoToUrl(url);
        HtmlReport.Pass("[" + GetDateTimeStamp() + "] Go to URL [" + url + "]");
    }

    #region  MOVEMENTS
    public void MoveForward()
    {
        driver.Navigate().Forward();

    }
    public void MoveBackward()
    {
        driver.Navigate().Back();

    }
    public void RefreshPage()
    {
        driver.Navigate().Refresh();

    }
    public void ScrollToBottomOfPage()
    {
        Javascript.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        Thread.Sleep(1000);
    }
    public void ScrollToTopOfPage()
    {
        Javascript.ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
        Thread.Sleep(1000);
    }
    #endregion

    #region INTERACTING WITH ELEMENTS
    public By GetXpath(string locator)
    {
        return By.XPath(locator);
    }
    public string GetTitle()
    {
        return driver.Title;
    }
    public string GetUrl()
    {
        return driver.Url;
    }
    public string GetText(string locator)
    {
        return driver.FindElement(By.XPath(locator)).Text;
    }
    public string GetTextFromIWebElement(IWebElement e)
    {
        return e.Text;
    }

    public IWebElement FindElementByXpath(string locator)
    {
        IWebElement e = driver.FindElement(GetXpath(locator));
        HighlightElem(e);
        return e;
    }

    public IList<IWebElement> FindElementsByXpath(string locator)
    {
        return driver.FindElements(GetXpath(locator));
    }

    public IWebElement FindParentElement(string locator)
    {
        return FindElementByXpath(locator).FindElement(By.XPath(".."));

    }

    public void PressEnter(string locator)
    {
        try
        {
            FindElementByXpath(locator).SendKeys(Keys.Enter);
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Press enter on element [" + locator + "] passed");

        }
        catch (Exception excep)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Press enter on element [" + locator + "] failed", TakeScreenShot());
            throw excep;
        }
    }
    public void Clear(string locator)
    {
        try
        {
            FindElementByXpath(locator).Clear();
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Clear previous input in element [" + locator + "] passed");

        }
        catch (Exception excep)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Clear previous input in element [" + locator + "] failed", TakeScreenShot());
            throw excep;
        }
    }
    public void Click(string locator)
    {
        try
        {
            IWebElement btnToClick = WaitToBeClickable(locator);
            HighlightElem(btnToClick);
            btnToClick.Click();
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Clicking on element [" + locator + "] passed");

        }
        catch (Exception excep)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Clicking on element [" + locator + "] failed", TakeScreenShot());
            throw excep;
        }
    }
    public void ClickJS(string locator)
    {
        try
        {
            IWebElement btnToClick = WaitToBeClickable(locator);
            HighlightElem(btnToClick);
            Javascript.ExecuteScript("arguments[0].click();", btnToClick);
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Clicking on element [" + locator + "] passed");

        }
        catch (Exception excep)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Clicking on element [" + locator + "] failed");
            throw excep;
        }
    }
    public void DoubleClick(string locator)
    {
        try
        {
            IWebElement btnToDoubleClick = WaitToBeClickable(locator);
            HighlightElem(btnToDoubleClick);
            _actions.DoubleClick(btnToDoubleClick).Perform();
            HtmlReport.Pass("[" + GetDateTimeStamp() + "]Double click on element [" + locator + "] successfuly");
        }
        catch (Exception ex)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Double click on element [" + locator + "] failed");
            throw ex;
        }
    }
    public void HoverBtn(string locator)
    {
        try
        {
            IWebElement btnToHover = WaitToBeVisible(locator);
            HighlightElem(btnToHover);
            _actions.MoveToElement(btnToHover);
            _actions.Build().Perform();
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Hover on element [" + locator + "] successfuly");
        }
        catch (Exception ex)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Hover on element [" + locator + "] failed");
            throw ex;
        }
    }
    public void DoubleClickJS(string locator)
    {
        try
        {
            IWebElement btnToDoubleClick = WaitToBeClickable(locator);
            HighlightElem(btnToDoubleClick);
            Javascript.ExecuteScript("arguments[0].dblclick();", btnToDoubleClick);
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Double click on element [" + locator + "] successfuly");
        }
        catch (Exception ex)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Double click on element [" + locator + "] failed");
            throw ex;
        }
    }
    public void RightClick(string locator)
    {
        try
        {
            IWebElement btnToRightClick = WaitToBeClickable(locator);
            HighlightElem(btnToRightClick);
            _actions.ContextClick(btnToRightClick).Perform();
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Right click on element [" + locator + "] successfuly");
        }
        catch (Exception ex)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Right click on element [" + locator + "] failed");
            throw ex;
        }
    }

    public void SendKeys(string locator, string key)
    {
        try
        {
            FindElementByXpath(locator).SendKeys(key);
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Sendkeys to element [" + locator + "] passed");
        }
        catch (Exception excep)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Sendkeys to element [ " + locator + " ] failed", TakeScreenShot());
            throw excep;
        }
    }
    public void ReplaceKey(string locator, string key)
    {
        try
        {
            Clear(locator);
            SendKeys(locator, key);
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Clearing previous input in [" + locator + "] and " +
                "replacing it with [" + key + "] passed");
        }
        catch (Exception excep)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Clearing previous input in [" + locator + "] and " +
                "replacing it with [" + key + "] failed", TakeScreenShot());
            throw excep;
        }
    }

    public IWebElement HighlightElem(IWebElement e)
    {
        try
        {
            string highlightJavascript = "arguments[0].style.border='2px solid red'";
            Javascript.ExecuteScript(highlightJavascript, new object[] { e });
            return e;

        }
        catch (Exception excep)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Highlight element [" + e.ToString() + "] failed", TakeScreenShot());
            throw excep;

        }
    }
    public void SelectOption(string locator, string key)
    {
        try
        {
            IWebElement mySelectOption = FindElementByXpath(locator);
            SelectElement dropdown = new SelectElement(mySelectOption);
            dropdown.SelectByText(key);
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Select element " + locator + " successfuly with " + key);
        }
        catch (Exception excep)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Select element " + locator + " failed with " + key, TakeScreenShot());
            throw excep;
        }
    }

    public void ClosePopUp(string locator)
    {
        try
        {
            IWebElement popUpCloseButton = WaitToBeClickable(locator);
            popUpCloseButton.Click();
            HtmlReport.Pass("[" + GetDateTimeStamp() + "] Close Pop up successfully");
        }
        catch (Exception excep)
        {
            HtmlReport.Fail("[" + GetDateTimeStamp() + "] Close Pop up fail", TakeScreenShot());
            throw excep;

        }
    }
    #endregion

    #region WAIT TIME
    /// <summary>
    /// Explicit waits to check Visible/Clickable/Selectable
    /// </summary>
    public IWebElement WaitToBeVisible(string locator)
    {
        var btnToClick = _explicitWait.Until(
            ExpectedConditions.ElementIsVisible(GetXpath(locator)));
        return btnToClick;
    }
    public IWebElement WaitToBeClickable(string locator)
    {
        var btnToClick = _explicitWait.Until(
            ExpectedConditions.ElementToBeClickable(GetXpath(locator)));
        return btnToClick;
    }
    public bool WaitToBeSelected(string locator)
    {
        var btnToClick = _explicitWait.Until(
            ExpectedConditions.ElementToBeSelected(GetXpath(locator)));
        return btnToClick;
    }
    public void WaitForQueryResult(int waitTime)
    {
        Thread.Sleep(waitTime);
    }
    #endregion

    #region CAPTURE SCREENSHOT   
    
    public string TakeScreenShot()
    {
        try
        {
            string fileName = HtmlReportDirectory.SCREENSHOT_PATH + ("/screenshot_" +
                DateTime.Now.ToString("yyyyMMddHHmmss")) + ".png";
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(fileName, ScreenshotImageFormat.Png);          

            return Path.GetRelativePath(HtmlReportDirectory.REPORT_FOLDER_PATH, fileName);

        }
        catch (Exception excep)
        {
            throw excep;
        }
    }

    public string TakeScreenShotElement(string locator, string savepath)
    {
        try
        {
            IWebElement e = FindElementByXpath(locator);
            Screenshot ss = ((ITakesScreenshot)e).GetScreenshot();
            ss.SaveAsFile(savepath, ScreenshotImageFormat.Png);
         

            return savepath;

        }
        catch (Exception excep)
        {         
            throw excep;
        }
    }

    #endregion

    #region VERIFYING / COMPARING / ASSERTING ACTIONS
    public bool CompareTwoImages(string locator, string imagename, double exprate)
    {        
        HtmlReportDirectory.InitImageReport(browserName, browserWidth, browserHeight, imagename);
        MergedPath = HtmlReportDirectory.MERGED_IMAGE_PATH;
        BaselinePath = HtmlReportDirectory.BASELINE_IMAGE_PATH;
        ActualPath = HtmlReportDirectory.ACTUAL_IMAGE_PATH;
      
        if (!File.Exists(BaselinePath))
        {           
            TakeScreenShotElement(locator, BaselinePath);
        }

        TakeScreenShotElement(locator, ActualPath);

        SimilarRate = ImageProcessor.CompareImage(BaselinePath, ActualPath, MergedPath, exprate);
        if (SimilarRate > exprate)
        {
            HtmlReport.Pass($"[{GetDateTimeStamp()}] Actual {imagename} image matches with expected image with similarity rate of [{SimilarRate.ToString("##")}%]");
            return true;

        }
        else
        {            
            HtmlReport.CreateImageLog(imageProcessor, imagename);
            return false;
        }
    }

    public bool VerifyElementIsDisplayed(string locator)
    {
        IWebElement e = driver.FindElement(GetXpath(locator));

        if (e == null)
        {
            return false;
        }
        else
        {
            HighlightElem(e);
            return true;
        }
    }
    #endregion

    #region DEALING WITH GRID
    public string GetRowIndex(string rowLocator, string cellLocator, int index)
    {
        // return an indexed row
        return rowLocator + "[" + index + "]" + cellLocator;
    }

    public IList<IWebElement> GetAllRows(string rowLocator)
    {
        IList<IWebElement> allRows = FindElementsByXpath(rowLocator);
        return allRows;
    }

    public List<string> GetTextFromAllCellsOfOneRow(string rowLocator, string cellLocator, int index)
    {
        /// Return a list of cell web elements fow an indexed row
        /// Get texts from all cells of one row and add them to a list of strings

        List<string> valuesFromCells = new List<string>();
        IList<IWebElement> allCells = FindElementsByXpath
            (GetRowIndex(rowLocator, cellLocator, index));
        foreach (IWebElement cell in allCells)
        {
            valuesFromCells.Add(GetTextFromIWebElement(cell));
        }
        return valuesFromCells;
    }
    public object ConvertToJson(object obj)
    {
        var list = JsonConvert.SerializeObject(obj);
        return list;
    }

    #endregion

    #region TIMESTAMP
    public static string GetDateTimeStamp()
    {
        var VNCulture = new CultureInfo("vi-VN");
        var utcDate = DateTime.UtcNow;
        var VNDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcDate, "SE Asia Standard Time");
        string currentTimeVN = VNDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss", VNCulture);
        return currentTimeVN;
    }

    #endregion

}

