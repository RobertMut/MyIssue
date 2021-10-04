using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using MyIssue.API.Filter;

namespace MyIssue.API.Services
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
