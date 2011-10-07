namespace Ifa.Model
{
    public class CurrentPage : Tag
    {
        private readonly int _current;

        public CurrentPage(int current,int pages)
            : base(null, pages)
        {
            _current = current;
        }

        protected override int GetPage()
        {
            return 0;
        }

        public override string GetText()
        {
            return _current.ToString();
        }
    }
}