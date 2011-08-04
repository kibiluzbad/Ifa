using Ifa.Configuration.Fluentlty;
using Ifa.Helpers;

namespace Ifa.Configuration
{
    public static class ConfigurationHelper
    {
        internal static IfaConfiguration Config = new IfaConfiguration{ItemsPerPage = 10,
            Left = 0,
            Right = 0,
            Window = 4};
        
        public static IfaConfiguration Get()
        {
            return Config;
        }

        public static void Configure(IfaConfiguration config)
        {
            Config = config;
        }

        public static void Configure(FluentConfiguration config)
        {
            Config = config.Configure();
        }
    }
}