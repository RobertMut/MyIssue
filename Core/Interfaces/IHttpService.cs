using System;
using System.Net;
using System.Threading.Tasks;

namespace MyIssue.Core.Interfaces
{
    public interface IHttpService
    {
        Task<string> Get(string path);
        Task<Uri> Post<T>(string path, T entity);
        Task<string> Put<T>(string path, T entity);
        Task<HttpStatusCode> Delete(string path, int id);
    }
}