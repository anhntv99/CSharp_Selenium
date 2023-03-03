using AventStack.ExtentReports.MarkupUtils;
using CoreFramework.APICore;

namespace CoreFramework.Reporter.ExtentMarkup;

public class APIResponseLog : IMarkup
{
    private APIRequest _request { get; set; }
    private APIResponse _response { get; set; }

    public APIResponseLog(APIRequest request, APIResponse response)
    {
        _request = request;
        _response = response;
    }
    public string GetMarkup()
    {
        string log = $@"
                    <p>Request URL: {_request.Url}</p>
                    <p>Response body: {_response.responseBody}</p>
                    <p>Response status: {_response.responseStatusCode}</p>";
        return log;
    }
}



