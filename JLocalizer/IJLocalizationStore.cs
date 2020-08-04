using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace JLocalizer
{
    public interface IJLocalizationStore
    {
        string CultureName { get; }

        LocalizedString GetOrNull(string name);

        void Fill(Dictionary<string, LocalizedString> dictionary);
    }
}
