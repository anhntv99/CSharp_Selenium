using DemoGuru99.PageObj;
using DemoGuru99.TestSetup;
using NUnit.Framework;

namespace DemoGuru99.TestCases;

public class Test : NUnitWebTestSetup
{
    public Test(string browser) : base(browser)
    {
    }

    [Test]
    [Category("TestImageComparison")]
    public void TestGuruLogo()
    {
        LoginPage loginPage = new LoginPage();

        loginPage.CompareGuruLogo("GuruLogo", 98);

    }



}
