using System;
using System.Collections.Generic;

namespace JLocalizer
{
    public class JLocalizationResource
    {
        public Type ResourceType { get; }

        public string ResourceName => JLocalizerAttribute.GetName(ResourceType);

        public JLocalizationResourceBinder Binder { get; }

        public string DefaultCultureName { get; set; }

        public JLocalizationResource(Type resourceType, string defaultCultureName = null)
        {
            ResourceType = resourceType;
            DefaultCultureName = defaultCultureName;
            Binder = new DefaultJLocalizationResourceBinder(this, new JLocalizationStoreBuilder(new DefaultJLocalizationResourceDeserialize()));
        }

        public JLocalizationResource(Type resourceType, JLocalizationResourceBinder resourceBinder)
        {
            ResourceType = resourceType;
            Binder = resourceBinder;
        }

        internal JLocalizationResource Execute()
        {
            Binder.ExecuteBinder();
            return this;
        }

        internal IReadOnlyDictionary<string, IJLocalizationStore> Get()
        {
            return Binder.Read();
        }
    }
}
