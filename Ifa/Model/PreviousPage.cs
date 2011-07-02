using System;

namespace Ifa.Model
{
    public class PreviousPage : Tag
    {
        private readonly int _current;

        public PreviousPage(int current, Func<int, int, string> urlFunc, int itemsPerPage)
            : base(urlFunc, itemsPerPage)
        {
            _current = current;
        }

        protected override int GetPage()
        {
            var newIndex = _current - 1;
            return newIndex > 0 ? newIndex : 1;
        }

        public override string GetText()
        {
            return "<";
        }
    }
}