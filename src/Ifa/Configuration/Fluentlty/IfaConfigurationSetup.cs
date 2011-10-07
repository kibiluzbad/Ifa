namespace Ifa.Configuration.Fluentlty
{
    public class IfaConfigurationSetup
    {
        private int _itemsPerPage;
        private int _window;
        private int _right;
        private int _left;

        public IfaConfigurationSetup ItemsPerPage(int itemsPerPage)
        {
            _itemsPerPage = itemsPerPage;
            return this;
        }

        public IfaConfigurationSetup Window(int window)
        {
            _window = window;
            return this;
        }

        public IfaConfigurationSetup Right(int right)
        {
            _right = right;
            return this;
        }

        public IfaConfigurationSetup Left(int left)
        {
            _left = left;
            return this;
        }

        internal IfaConfiguration Build()
        {
            return new IfaConfiguration
                       {
                           ItemsPerPage = _itemsPerPage,
                           Left = _left,
                           Right = _right,
                           Window = _window
                       };
        }
    }
}