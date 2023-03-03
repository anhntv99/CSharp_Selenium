using AventStack.ExtentReports.MarkupUtils;
using CoreFramework.APICore;
using CoreFramework.Utilities;

namespace CoreFramework.Reporter.ExtentMarkup;
public class MarkupHelperPlus
{
    public static IMarkup CreateAPIRequestLog(APIRequest request)
    {
        return new APIRequestLog(request);
    }
    public static IMarkup CreateAPIResponseLog(APIRequest request, APIResponse response)
    {
        return new APIResponseLog(request, response);
    }
    public static IMarkup CreateImageLog(ImageProcessor imageProcessor, string imagename)
    {
        return new ImageLog(imageProcessor, imagename);
    }
}

