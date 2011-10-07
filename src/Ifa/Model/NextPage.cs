using System;

namespace Ifa.Model
{
    public class NextPage : Tag
    {
        private readonly int _current;
        private readonly int _pages;

        public NextPage(int current, Func<int, int, string> urlFunc, int pages, int itensPerPage)
            : base(urlFunc, itensPerPage)
        {
            _current = current;
            _pages = pages;
        }

        protected override int GetPage()
        {
            var newIndex = _current + 1;
            return newIndex <= _pages ? newIndex : _pages;
        }

        public override string GetText()
        {
            return ">";
        }
    }
}