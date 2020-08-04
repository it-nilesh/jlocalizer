using System;
using System.Linq;

namespace JLocalizer
{
    public class JLocalizerAttribute : Attribute
    {
        public string ResourceName { get; }

        public JLocalizerAttribute(string resourceName)
        {
            ResourceName = resourceName;
        }

        public static JLocalizerAttribute GetOrNull(Type resourceType)
        {
            return resourceType
                .GetCustomAttributes(true)
                .OfType<JLocalizerAttribute>()
                .FirstOrDefault();
        }

        public static string GetName(Type resourceType)
        {
            return GetOrNull(resourceType)?.ResourceName ?? resourceType.Namespace;
        }
    }
}
