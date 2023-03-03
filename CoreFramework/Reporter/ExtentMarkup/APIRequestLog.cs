using AventStack.ExtentReports.MarkupUtils;
using CoreFramework.APICore;

namespace CoreFramework.Reporter.ExtentMarkup;

public class APIRequestLog : IMarkup
{
    private APIRequest _request { get; set; }
    public APIRequestLog(APIRequest request)
    {
        _request = request;
    }
    public string GetMarkup()
    {
        string log = $@"
                    <p>Request URL: {_request.Url}</p>
                    <p>Request body: {_request.RequestBody}</p>";
        return log;
    }
}



