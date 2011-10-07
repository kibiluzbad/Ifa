using System;

namespace Ifa.Model
{
    public class LastPage : Tag
    {
        private readonly int _pages;

        public LastPage(Func<int, int, string> urlFunc, int pages, int itensPerPage)
            : base(urlFunc, itensPerPage)
        {
            _pages = pages;
        }


        protected override int GetPage()
        {
            return _pages;
        }

        public override string GetText()
        {
            return ">>";
        }
    }
}