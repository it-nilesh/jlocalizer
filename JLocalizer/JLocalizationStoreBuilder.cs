using Microsoft.Extensions.Localization;
using System;
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
            var locDictionary = new Dictionary<string, LocalizedString>(StringComparer.Ordinal);
            var languageDictionary = _deserialize.Get(stream);

            if (languageDictionary == null)
                return new JLocalizationStore(cultureName, locDictionary);
            
            foreach (var language in languageDictionary)
            {
                locDictionary[language.Key] = new LocalizedString(language.Key, language.Value);
            }

            return new JLocalizationStore(cultureName, locDictionary);
        }
    }
}
