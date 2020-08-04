using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.IO;

namespace JLocalizer
{
    public class JLocalizationStoreBuilder
    {
        private readonly IJLocalizationResourceDeserialize _deserialize;
        public JLocalizationStoreBuilder(IJLocalizationResourceDeserialize deserialize)
        {
            _deserialize = deserialize;
        }

        public IJLocalizationStore BuildFromStream(string cultureName, Stream stream)
        {
            var locDictionary = new Dictionary<string, LocalizedString>();
            var languageDictionary = _deserialize.Get(stream);
            
            foreach (var language in languageDictionary)
            {
                locDictionary[language.Key] = new LocalizedString(language.Key, language.Value);
            }

            return new JLocalizationStore(cultureName, locDictionary);
        }
    }
}
