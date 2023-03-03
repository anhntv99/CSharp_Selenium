using CoreFramework.DriverCore;

namespace DemoGuru99.PageObj;
public class LoginPage : WebDriverAction
{
    public string guruLogo = "//*[@alt='Guru99 Demo Sites']";
         

    public LoginPage() : base()
    {
    }
    public void CompareGuruLogo(string imageName, double exprate)
    {
        CompareTwoImages(guruLogo, imageName, exprate);
    }

}
