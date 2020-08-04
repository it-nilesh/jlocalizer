using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;

namespace JLocalizer
{
    public class ExternalLocalizerFactory : IExternalLocalizerFactory
    {
        private readonly LocalizationFactoryOptions _localizationFactory;
        private readonly IServiceProvider _serviceProvider;

        public ExternalLocalizerFactory(IOptions<LocalizationFactoryOptions> localizationFactory, IServiceProvider serviceProvider)
        {
            _localizationFactory = localizationFactory.Value;
            _serviceProvider = serviceProvider;
        }

        public IStringLocalizer Create(Type resource)
        {
            return _localizationFactory.Create(resource, _serviceProvider);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return _localizationFactory.Create(_serviceProvider, baseName, location);
        }
    }
}
