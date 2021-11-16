using System;
using System.Collections.Generic;
using MyIssue.Main.API.Filter;
using MyIssue.Main.API.Services;
using MyIssue.Main.API.Wrappers;

namespace MyIssue.Main.API.Helpers
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
