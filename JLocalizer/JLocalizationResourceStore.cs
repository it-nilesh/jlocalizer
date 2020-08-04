using System;
using System.Collections.Generic;

namespace JLocalizer
{
    public class JLocalizationResourceStore : Dictionary<Type, JLocalizationResource>
    {
        public JLocalizationResource Add<TResouce>()
        {
            return Add(typeof(TResouce));
        }

        public JLocalizationResource Add(Type resourceType)
        {
            return this[resourceType] = new JLocalizationResource(resourceType);
        }

        public JLocalizationResource Add(Type resourceType, JLocalizationResourceBinder resourceBinder)
        {
            return this[resourceType] = new JLocalizationResource(resourceType, resourceBinder);
        }

        public JLocalizationResource Get<TResource>()
        {
            var resourceType = typeof(TResource);
            return this.Get(resourceType);
        }
    }
}
