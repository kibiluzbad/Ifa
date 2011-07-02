using System;

namespace Ifa.Model
{
    public class FirstPage : Tag
    {
        public FirstPage(Func<int, int, string> urlFunc, int itemsPerPage)
            : base(urlFunc, itemsPerPage)
        { }

        protected override int GetPage()
        {
            return 1;
        }

        public override string GetText()
        {
            return "<<";
        }
    }
}