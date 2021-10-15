using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MyIssue.Core.Interfaces;

namespace MyIssue.Core.Service
{
    public class HttpService : IHttpService
    {
        private HttpClient _client;

        public HttpService(string address)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:5002/");
        }

        public async Task<string> Get(string path)
        {
            string returnString = System.String.Empty;
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                returnString = await response.Content.ReadAsStringAsync();
            }

            return returnString;
        }

        public async Task<Uri> Post<T>(string path, T entity)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(path, entity);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        public async Task<string> Put<T>(string path, T entity)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(path, entity);
            response.EnsureSuccessStatusCode();

            entity = await response.Content.ReadAsAsync<T>();
            return Newtonsoft.Json.JsonConvert.SerializeObject(entity);
        }

        public async Task<HttpStatusCode> Delete(string path, int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(path + id);
            return response.StatusCode;
        }
    }

}
