using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Http
{
    public class RequestMessage
    {
        public static HttpRequestMessage NewRequest(string address, HttpMethod method, string token)
        {
            var request = new HttpRequestMessage(method,
                address);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            request.Headers.Connection.Add("keep-alive");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return request;
        }

    }
}