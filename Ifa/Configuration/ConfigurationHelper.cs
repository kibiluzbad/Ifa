using Ifa.Configuration.Fluentlty;
using Ifa.Helpers;

namespace Ifa.Configuration
{
    public static class ConfigurationHelper
    {
        private static object _syncLock = new object();
        private static IfaConfiguration _config = new IfaConfiguration
        {
            ItemsPerPage = 10,
            Left = 0,
            Right = 0,
            Window = 4
        };

        public static IfaConfiguration Get()
        {
            return _config;
        }

        public static void Configure(IfaConfiguration config)
        {
            lock (_syncLock)
            {
                _config = config;
            }
        }

        public static void Configure(FluentConfiguration config)
        {
            lock (_syncLock)
            {
                _config = config.Configure();
            }
        }
    }
}