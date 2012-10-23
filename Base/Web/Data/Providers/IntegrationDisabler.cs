using Sitecore.Common;

namespace SitecoreSuperchargers.GenericItemProvider.Data.Providers
{
    public class IntegrationDisabler : Switcher<bool, IntegrationDisabler>
    {
        // Methods
        public IntegrationDisabler()
            : base(true)
        {
        }
    }
}
