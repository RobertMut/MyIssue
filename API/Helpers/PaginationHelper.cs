using System;
using System.Collections.Generic;
using MyIssue.API.Filter;
using MyIssue.API.Services;
using MyIssue.API.Wrappers;

namespace MyIssue.API.Helpers
{
    public class PaginationHelper
    {
        public static PageResponse<List<T>> CreatePageResponse<T>(List<T> pageData, PaginationFilter filter,
            int totalRecords, IUriService uriService, string route)
        {
            var response = new PageResponse<List<T>>(filter.PageNumber, filter.PageSize, pageData);
            var totalPages = ((double) totalRecords / (double) filter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            response.NextPage =
                filter.PageNumber >= 1 && filter.PageNumber < roundedTotalPages
                    ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber + 1, filter.PageSize), route)
                    : null;
            response.PreviousPage =
                filter.PageNumber - 1 >= 1 && filter.PageNumber <= roundedTotalPages
                    ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber - 1, filter.PageSize), route)
                    : null;
            response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, filter.PageSize), route);
            response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, filter.PageSize), route);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;

        }
    }
}
