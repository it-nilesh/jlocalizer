using System;
using System.Collections.Generic;

namespace JLocalizer
{
    public class JLocalizationResource
    {
        private readonly object _loadLock = new object();
        private bool _isLoaded;
        private readonly string _resourceName;

        public Type ResourceType { get; }

        public string ResourceName => _resourceName;

        public JLocalizationResourceBinder Binder { get; }

        public string DefaultCultureName { get; set; }

        public JLocalizationResource(Type resourceType, string defaultCultureName = null)
        {
            ResourceType = resourceType;
            _resourceName = JLocalizerAttribute.GetName(resourceType);
            DefaultCultureName = defaultCultureName;
            Binder = new DefaultJLocalizationResourceBinder(this, new JLocalizationStoreBuilder(new DefaultJLocalizationResourceDeserialize()));
        }

        public JLocalizationResource(Type resourceType, JLocalizationResourceBinder resourceBinder)
        {
            ResourceType = resourceType;
            _resourceName = JLocalizerAttribute.GetName(resourceType);
            Binder = resourceBinder;
        }

        internal JLocalizationResource Execute()
        {
            if (!_isLoaded)
            {
                lock (_loadLock)
                {
                    if (!_isLoaded)
                    {
                        Binder.ExecuteBinder();
                        _isLoaded = true;
                    }
                }
            }

            return this;
        }

        internal void Reload()
        {
            lock (_loadLock)
            {
                Binder.ExecuteBinder();
                _isLoaded = true;
            }
        }

        internal IReadOnlyDictionary<string, IJLocalizationStore> Get()
        {
            Execute();
            return Binder.Read();
        }
    }
}
