using AventStack.ExtentReports.MarkupUtils;
using CoreFramework.APICore;


namespace CoreFramework.Reporter.ExtentMarkup
{
    public class MarkupHelperPlus
    {
        public static IMarkup CreateAPIRequestLog(APIRequest request, APIResponse response)
        {
            return new APIRequestLog(request, response);
        }  

    }
}
