using CoreFramework.DriverCore;
using CoreFramework.Reporter;
using CoreFramework.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CoreFramework.NUnitTestSetup;
[TestFixtureSource(typeof(CrossBrowserData),
    nameof(CrossBrowserData.GetBrowserConfig))]

public class NUnitTestSetup
{
    protected WebDriverAction? DriverBaseAction;
    private string Device = "PC";
    private string Category = "Phase2_TestProject";
    private readonly string _browser;



    public NUnitTestSetup(string browser)
    {
        _browser = browser;
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        HtmlReport.CreateReport();
        HtmlReport.CreateTest(TestContext.CurrentContext.Test.ClassName).AssignDevice(Device).AssignCategory(Category);
    }

    [SetUp]
    public void SetUp()
    {
        WebDriverManager.InitDriver(_browser);
        HtmlReport.CreateNode(TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);

    }

    [TearDown]
    public void TearDown()
    {
        TestStatus testStatus = TestContext.CurrentContext.Result.Outcome.Status;
        if (testStatus.Equals(TestStatus.Passed))
        {
            HtmlReport.Pass($"[PASSED: Test case passed]");
        }
        else
        {
            HtmlReport.Fail("FAILED: Test errors: " + TestContext.CurrentContext.Result.Message, DriverBaseAction.TakeScreenShot());
        }
        WebDriverManager.CloseDriver();
    }
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        HtmlReport.Flush();
    }
}


