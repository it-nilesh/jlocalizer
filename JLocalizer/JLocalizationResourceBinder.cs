using System.Collections.Generic;

namespace JLocalizer
{
    public abstract class JLocalizationResourceBinder
    {
        protected IReadOnlyDictionary<string, IJLocalizationStore> LocalizationStore =
            new Dictionary<string, IJLocalizationStore>();

        internal void ExecuteBinder()
        {
            LocalizationStore = Execute();
        }

        internal IReadOnlyDictionary<string, IJLocalizationStore> Read()
        {
            return LocalizationStore;
        }

        public abstract IReadOnlyDictionary<string, IJLocalizationStore> Execute();
    }
}
