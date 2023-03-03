using System.Net;
using System.Text;
using CoreFramework.Reporter;

namespace CoreFramework.APICore;
public class APIRequest
{
    public HttpWebRequest Request;
    public string Url { get; set; }
    public string RequestBody { get; set; }
    public string FormData { get; set; }
    public APIRequest()
    {
        Url = "";
        RequestBody = "";
        FormData = "";
    }
    public APIRequest AddHeader(string key, string value)
    {
        Request.Headers.Add(key, value);
        return this;
    }

    public APIRequest AddFormData(string key, string value)
    {
        if(FormData.Equals("") || FormData == null)
        {
            FormData += key + "=" + value;
        }
        else if (!FormData.Equals(""))
        {
            FormData += "&" + key + "=" + value;
        }
        return this;
    }
    public APIRequest SetBody(string body)
    {
        this.RequestBody = body;
        return this;
    }
    public APIRequest SetURL(string url)
    {
        Url = url;
        Request = (HttpWebRequest)WebRequest.Create(url);
        return this;
    }

    public APIRequest SetRequestParameter(string key, string value)
    {
        if (Url.Contains("?"))
        {
            Url += "?" + key + "=" + value;
        }
        else
        {
            // If there is already a parameter
            Url += "&" + key + "=" + value;
        }
        return this;
    }
    public APIResponse Get()
    {
        Request.Method = "GET";
        APIResponse response = SendRequest();
        return response;
    }
    public APIResponse Post()
    {
        Request.Method = "POST";
        APIResponse response = SendRequest();
        return response;
    }
    public APIResponse Put()
    {
        Request.Method = "PUT";
        APIResponse response = SendRequest();
        return response;
    }
    public APIResponse Delete()
    {
        Request.Method = "DELETE";
        APIResponse response = SendRequest();
        return response;
    }
    public APIResponse SendRequest()
    {
        if (Request.Method == "GET")
        {
            RequestBody = null;
        }
        else
        {
            if (RequestBody != null)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(RequestBody);
                Request.ContentLength = byteArray.Length;
                using (Stream dataStream = Request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Flush();
                    dataStream.Close();
                }
            }
            if (!FormData.Equals(""))
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(FormData);
                Request.ContentLength = byteArray.Length;
                using (Stream dataStream = Request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Flush();
                    dataStream.Close();
                }
            }
        }
        try
        {
            IAsyncResult asyncResult = Request.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            var httpResponse = (HttpWebResponse)Request.EndGetResponse(asyncResult);
            APIResponse response = new APIResponse(httpResponse);
            HtmlReport.CreateAPIRequestLog(this);
            HtmlReport.CreateAPIResponseLog(this, response);
            return response;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

}


