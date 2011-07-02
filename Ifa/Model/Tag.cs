using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ifa.Model
{
    public abstract class Tag
    {
        private readonly Func<int, int, string> _urlFunc;
        private readonly int _itensPerPage;

        protected Tag(Func<int, int, string> urlFunc, int itemsPerpages)
        {
            _urlFunc = urlFunc;
            _itensPerPage = itemsPerpages;
        }

        protected abstract int GetPage();
        public abstract string GetText();

        public virtual string GetUrl()
        {
            var page = GetPage();
            return 0 < page 
                ? _urlFunc.Invoke(_itensPerPage, page)
                : string.Empty ;
        }
    }
}
