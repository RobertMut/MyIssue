namespace MyIssue.Web.Model
{
    public class PageResponse<T>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? FirstPage { get; set; }
        public string? LastPage { get; set; }
        public int? TotalPages { get; set; }
        public int? TotalRecords { get; set; }
        public string? NextPage { get; set; }
        public string? PreviousPage { get; set; }
        public T[]? Data { get; set; }

        public PageResponse(int pageNumber, int pageSize, T[] data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
        }

        public PageResponse()
        {

        }
    }
}
