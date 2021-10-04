using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.Web.Model
{
    public class Paged
    {
        public string? Link { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
        public string? FirstPage { get; set; }
        public string? LastPage { get; set; }
        public string? PreviousLink { get; set; }
        public string? NextLink { get; set; }
        public Task[]? Data { get; set; }

        public Paged(string link)
        {
            Link = link;
        }

        public Paged(int page, int size)
        {
            Page = page;
            Size = size;
        }

        public Paged(string? previousLink, string? nextLink, string lastPage, string firstPage, Task[] data)
        {
            FirstPage = firstPage;
            LastPage = lastPage;
            PreviousLink = previousLink;
            NextLink = nextLink;
            Data = data;
        }

        public Paged()
        {

        }
    }
}
