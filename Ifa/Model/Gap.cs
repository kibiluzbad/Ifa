using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ifa.Model
{
    public class Gap : Tag
    {
        public Gap() : base(null,0)
        { }

        protected override int GetPage()
        {
            return 0;
        }

        public override string GetText()
        {
            return "...";
        }
    }
}
