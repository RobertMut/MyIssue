using System;
using Microsoft.AspNetCore.WebUtilities;
using MyIssue.Main.API.Filter;

namespace MyIssue.Main.API.Services
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }

    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var endpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri =
                QueryHelpers.AddQueryString(endpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
