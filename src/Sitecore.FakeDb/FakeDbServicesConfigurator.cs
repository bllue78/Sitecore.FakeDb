namespace Sitecore.FakeDb
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Abstractions;
    using Sitecore.DependencyInjection;
    using Sitecore.FakeDb.Links;

    public class FakeDbServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BaseLinkManager, SwitchingLinkManager>();
        }
    }
}