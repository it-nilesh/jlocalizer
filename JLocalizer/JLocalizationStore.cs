using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace JLocalizer
{
    public class JLocalizationStore : IJLocalizationStore
    {
        public JLocalizationStore(string cultureName, Dictionary<string, LocalizedString> store)
        {
            CultureName = cultureName;
            Store = store;
        }

        protected Dictionary<string, LocalizedString> Store { get; }

        public string CultureName { get; }

        public void Fill(Dictionary<string, LocalizedString> dictionary)
        {
            foreach (var item in Store)
            {
                dictionary[item.Key] = item.Value;
            }
        }

        public LocalizedString GetOrNull(string name)
        {
            return Store.Get(name);
        }
    }

    public static class JLocalizationStoreExtension
    {
        public static Dictionary<string, IJLocalizationStore> AddJLocalization(this Dictionary<string, IJLocalizationStore> jlocalizationStore, string cultureName, Dictionary<string, string> dictionary)
        {
            var store = new Dictionary<string, LocalizedString>();
            foreach (var localizationValue in dictionary)
            {
                store.Add(localizationValue.Key, new LocalizedString(localizationValue.Key, localizationValue.Value));
            }

            jlocalizationStore.Add(cultureName, new JLocalizationStore(cultureName, store));

            return jlocalizationStore;
        }
    }
}
