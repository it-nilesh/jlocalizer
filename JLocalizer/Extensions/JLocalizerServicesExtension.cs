using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace JLocalizer
{
    public static class JLocalizerServicesExtension
    {
        public static void AddJLocalizer(this IServiceCollection services, Action<JLocalizerResourceService> localizerResource, ServiceLifetime serviceLifetime)
        {
            services.Replace(ServiceDescriptor.Describe(typeof(IStringLocalizerFactory), typeof(JStringLocalizerFactory), serviceLifetime));
            services.Add(ServiceDescriptor.Describe(typeof(IStringLocalizer<>), typeof(JStringLocalizer<>), serviceLifetime));

            ServiceDescriptor.Describe(typeof(IStringLocalizer), x =>
            {
                var options = x.GetService<IOptions<JLocalizationOptions>>();
                var factory = x.GetService<IStringLocalizerFactory>();
                return factory.Create(options.Value.Resources.ElementAtOrDefault(0).Key);
            }, serviceLifetime);

            services.AddSingleton<IExternalLocalizerFactory, ExternalLocalizerFactory>();
            localizerResource(new JLocalizerResourceService(services));
        }

        public static void AddJLocalizer(this IServiceCollection services, Action<JLocalizerResourceService> localizerResource)
        {
            AddJLocalizer(services, localizerResource, ServiceLifetime.Singleton);
        }
    }
}
