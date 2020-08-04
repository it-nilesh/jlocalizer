using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace JLocalizer
{
    public abstract class StringLocalizerAdapter : IStringLocalizer
    {
        public LocalizedString this[string name] => GetLocalizedString(name, CultureInfo.CurrentUICulture.Name);

        public LocalizedString this[string name, params object[] arguments] => GetStringFormatted(name, CultureInfo.CurrentUICulture.Name, arguments);

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return GetAllStrings(CultureInfo.CurrentUICulture.Name, includeParentCultures);
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return WithCulture(culture, this);
        }

        public abstract LocalizedString GetLocalizedString(string name, string cultureName);

        public abstract LocalizedString GetStringFormatted(string name, string cultureName, params object[] arguments);

        public abstract IReadOnlyList<LocalizedString> GetAllStrings(string cultureName, bool includeParentCultures = true);

        public abstract IStringLocalizer WithCulture<T>(CultureInfo culture, T stringLocalizer) where T : IStringLocalizer;
    }

    public class StringLocalizerAdapter<T> : IStringLocalizer<T>
    {
        private readonly IStringLocalizer _stringLocalizer;
        public StringLocalizerAdapter(LocalizerFactory<T> localizerFactory)
        {
            _stringLocalizer = localizerFactory.Create(typeof(T));
        }

        public LocalizedString this[string name] => _stringLocalizer[name];

        public LocalizedString this[string name, params object[] arguments] => _stringLocalizer[name, arguments];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _stringLocalizer.GetAllStrings(includeParentCultures);
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return _stringLocalizer.WithCulture(culture);
        }
    }
}
