using System;

namespace Ifa.Configuration.Fluentlty
{
    public class FluentConfiguration
    {
        private IfaConfigurationSetup _configurationSetup;

        public FluentConfiguration Setup(IfaConfigurationSetup configurationSetup)
        {
            _configurationSetup = configurationSetup;
            return this;
        }

        public IfaConfiguration Configure()
        {
            if(null == _configurationSetup)
                throw new InvalidOperationException("First call setup and initalize de configuration attributes.");
            return _configurationSetup.Build();
        }
    }
}