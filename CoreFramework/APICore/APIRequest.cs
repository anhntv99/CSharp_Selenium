using CoreFramework.Reporter;
using System.Net;
using System.Text;

namespace CoreFramework.APICore
{
    public class APIRequest
    {
        public HttpWebRequest request;
        public string url { get; set; }
        public string requestBody { get; set; }
        public string formData { get; set; }



        public APIRequest SetUrl(string url)
        {
            this.url = url;
            request = (HttpWebRequest)WebRequest.Create(url);
            return this;
        }

        public APIRequest()
        {
            url = "";
            requestBody = "";
            formData = "";
        }

        public APIRequest(string baseUrl)
        {
            this.url = baseUrl;
            requestBody = "";
            formData = "";
        }

        public APIRequest AddHeader(string key, string value)
        {
            request.Headers.Add(key, value);
            return this;
        }
        public APIRequest SetRequestParameter(string key, string value)
        {
            if (url.Contains("?"))
            {
                url = url + "&" + key + "=" + value;
            }
            else
            {
                url = url + "?" + key + "=" + value;
            }
            return this;
        }

        public APIRequest AddFormData(string key, string value)
        {
            if (formData.Equals("") || formData == null)
            {
                formData += key + "=" + value;
            }
            else
            {
                formData += "&" + key + "=" + value;
            }
            return this;
        }

        public APIRequest SetBody(string body)
        {
            this.requestBody = body;
            return this;
        }


        public APIResponse SendRequest()
        {
            if (request.Method == "GET")
            {
                requestBody = null;
            }
            else
            {
                if (requestBody != null)
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);
                    request.ContentLength = byteArray.Length;
                    using (Stream dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Flush();
                        dataStream.Close();
                    }
                }
                if (!formData.Equals("."))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);
                    request.ContentLength = byteArray.Length;
                    using (Stream dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Flush();
                        dataStream.Close();
                    }
                }
            }
            try
            {
                IAsyncResult asyncResult = request.BeginGetResponse(null, null);
                asyncResult.AsyncWaitHandle.WaitOne();
                var httpResponse = (HttpWebResponse)request.EndGetResponse(asyncResult);
                APIResponse response = new APIResponse(httpResponse);
                HtmlReport.CreateAPIRequestLog(this, response);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public APIRequest Get()
        {
            request.Method = "GET";
            APIResponse response = SendRequest();
            return response;
        }

        public APIRequest Post()
        {
            request.Method = "POST";
            APIResponse response = SendRequest();
            return response;
        }

        public APIRequest Head()
        {
            request.Method = "HEAD";
            APIResponse response = SendRequest();
            return response;
        }

        public APIRequest Options()
        {
            request.Method = "OPTIONS";
            APIResponse response = SendRequest();
            return response;
        }

        public APIRequest Delete()
        {
            request.Method = "DELETE";
            APIResponse response = SendRequest();
            return response;
        }
    }
}
