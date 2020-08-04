using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;

namespace JLocalizer
{
    public class LocalizationFactoryOptions : IDisposable
    {
        private readonly ConcurrentDictionary<Type, Func<IServiceProvider, IStringLocalizerFactory>> externalFactory =
             new ConcurrentDictionary<Type, Func<IServiceProvider, IStringLocalizerFactory>>();

        public void AddExternalFactory<T>() where T : IStringLocalizerFactory
        {
            IStringLocalizerFactory func(IServiceProvider serviceProvider)
            {
                return ActivatorUtilities.GetServiceOrCreateInstance<T>(serviceProvider);
            }

            externalFactory[typeof(T)] = func;
        }

        internal IStringLocalizer Create(Type resource, IServiceProvider serviceProvider)
        {
            IStringLocalizer stringLocalizer = default;

            foreach (var factory in externalFactory)
            {
                stringLocalizer = factory.Value(serviceProvider).Create(resource);
                if (stringLocalizer != null)
                    break;
            }

            return stringLocalizer;
        }

        internal IStringLocalizer Create(IServiceProvider serviceProvider, string baseName, string location)
        {
            IStringLocalizer stringLocalizer = default;

            foreach (var factory in externalFactory)
            {
                stringLocalizer = factory.Value(serviceProvider).Create(baseName, location);
                if (stringLocalizer != null)
                    break;
            }

            return stringLocalizer;
        }

        public void Dispose()
        {
            externalFactory.Clear();
            GC.SuppressFinalize(true);
        }
    }
}
