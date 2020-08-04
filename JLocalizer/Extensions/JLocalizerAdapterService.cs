using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace JLocalizer
{
    public class JLocalizerAdapterService
    {
        private readonly IServiceCollection _services;
        public JLocalizerAdapterService(IServiceCollection services)
        {
            _services = services;
        }

        public void AddLocalizerAdapter<TType>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            _services.Add(ServiceDescriptor.Describe(typeof(IStringLocalizer<TType>), typeof(StringLocalizerAdapter<TType>), serviceLifetime));
        }
    }
}
