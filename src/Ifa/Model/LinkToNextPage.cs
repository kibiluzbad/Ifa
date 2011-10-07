using System;

namespace Ifa.Model
{
    public class LinkToNextPage : NextPage
    {
        public LinkToNextPage(int current, Func<int, int, string> urlFunc, int pages, int itensPerPage) 
            : base(current, urlFunc, pages, itensPerPage)
        {

        }

        public override string GetText()
        {
            return "More...";
        }
    }
}