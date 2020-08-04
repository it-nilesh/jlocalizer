using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;

namespace JLocalizer
{
    public class JLocalizerResourceService
    {
        private readonly IServiceCollection _services;
        public JLocalizerResourceService(IServiceCollection services)
        {
            _services = services;
        }

        public void DefaultCulture(string name)
        {
            CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(name);
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture(name);
        }

        public JLocalizerAdapterService AddLocalizerFactory<TFactory>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TFactory : IStringLocalizerFactory
        {
            _services.Replace(ServiceDescriptor.Describe(typeof(TFactory), typeof(TFactory), serviceLifetime));
            _services.Configure<LocalizationFactoryOptions>(option =>
            {
                option.AddExternalFactory<TFactory>();
            });

            return new JLocalizerAdapterService(_services);
        }

        public JLocalizerAdapterService AddLocalizerFactory<TType, TFactory>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TFactory : LocalizerFactory<TType>
        {
            _services.Add(ServiceDescriptor.Describe(typeof(LocalizerFactory<TType>), typeof(TFactory), serviceLifetime));
            _services.Add(ServiceDescriptor.Describe(typeof(IStringLocalizer<TType>), typeof(StringLocalizerAdapter<TType>), serviceLifetime));
            _services.Configure<LocalizationFactoryOptions>(option =>
            {
                option.AddExternalFactory<TFactory>();
            });

            return new JLocalizerAdapterService(_services);
        }

        public JLocalizerResourceService AddLocalizedResource(Type resource)
        {
            _services.Configure<JLocalizationOptions>(options =>
            {
                options.Resources.Add(resource);
            });

            return this;
        }

        public JLocalizerResourceService AddLocalizedResource<T>(T resourceBinder) where T : JLocalizationResourceBinder
        {
            _services.Configure<JLocalizationOptions>(options =>
            {
                options.Resources.Add(typeof(T), resourceBinder);
            });

            return this;
        }
    }
}
