using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace JLocalizer
{
    internal class JStringLocalizer<T> : IStringLocalizer<T>
    {
        public JStringLocalizer(IStringLocalizerFactory localizerFactory)
        {
            Localizer = localizerFactory.Create(typeof(T));
        }

        public LocalizedString this[string name] => Localizer[name];

        public LocalizedString this[string name, params object[] arguments] => Localizer[name, arguments];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return Localizer.GetAllStrings(includeParentCultures);
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return Localizer.WithCulture(culture);
        }

        internal IStringLocalizer Localizer;
    }
}
