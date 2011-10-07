using System;

namespace Ifa.Model
{
    public class PageLink : Tag
    {
        private readonly int _index;

        public PageLink(int index, Func<int, int, string> urlFunc, int itemPerPage)
            : base(urlFunc, itemPerPage)
        {
            _index = index;
        }

        protected override int GetPage()
        {
            return _index;
        }

        public override string GetText()
        {
            return _index.ToString();
        }
    }
}