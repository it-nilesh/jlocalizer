using JLocalizer.VirtualFile;
using System.Collections.Generic;
using System.IO;

namespace JLocalizer
{
    public class DefaultJLocalizationResourceBinder : JLocalizationResourceBinder
    {
        private readonly JLocalizationResource _resource;
        private readonly JLocalizationStoreBuilder _storeBuilder;
        private readonly VirtualFileInfo _virtualFileInfo;

        public DefaultJLocalizationResourceBinder(JLocalizationResource resource, JLocalizationStoreBuilder storeBuilder)
        {
            _resource = resource;
            _storeBuilder = storeBuilder;
            _virtualFileInfo = new VirtualFileInfo(_resource.ResourceType.Assembly).Get(".json");
        }

        public override IReadOnlyDictionary<string, IJLocalizationStore> Execute()
        {
            return _virtualFileInfo.Read(_resource.ResourceName, "json", BuildFromStream);
        }

        private IJLocalizationStore BuildFromStream(string cultureName, Stream stream)
        {
            return _storeBuilder.BuildFromStream(cultureName, stream);
        }
    }
}
