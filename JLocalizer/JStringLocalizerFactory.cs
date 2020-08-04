using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace JLocalizer
{
    //TODO:
    //Pending Testing
    //Dataannotations
    //View page 
    public class JStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly JLocalizationOptions _localizationOptions;
        private readonly ConcurrentDictionary<Type, IStringLocalizer> _localizerCache;
        private readonly IExternalLocalizerFactory _externalLocalizerFactory;

        public JStringLocalizerFactory(IOptions<JLocalizationOptions> localizationOptions, IExternalLocalizerFactory externalLocalizerFactory)
        {
            _localizationOptions = localizationOptions.Value;
            _externalLocalizerFactory = externalLocalizerFactory;
            _localizerCache = new ConcurrentDictionary<Type, IStringLocalizer>();
        }

        public IStringLocalizer Create(Type resource)
        {
            var localizationResource = _localizationOptions.Resources.Get(resource);

            if (localizationResource == null)
                return _externalLocalizerFactory.Create(resource);

            return _localizerCache.GetOrAdd(resource, value =>
            {
                return new JStringLocalizer(localizationResource);
            });
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return _externalLocalizerFactory.Create(baseName, location);
        }
    }
}
