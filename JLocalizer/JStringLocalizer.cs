using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace JLocalizer
{
    internal class JStringLocalizer : IStringLocalizer
    {
        public JStringLocalizer(JLocalizationResource resource)
        {
            Resource = resource.Execute();
        }

        public JLocalizationResource Resource { get; }

        public LocalizedString this[string name] => GetString(name);

        public LocalizedString this[string name, params object[] arguments] => GetStringFormatted(name, arguments);

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return GetAllStrings(CultureInfo.CurrentUICulture.Name, includeParentCultures);
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new WithCultureStringLocalizer(culture.Name, this);
        }

        private LocalizedString GetString(string name)
        {
            return GetLocalizedString(name, CultureInfo.CurrentUICulture.Name);
        }

        private LocalizedString GetStringFormatted(string name, params object[] arguments)
        {
            return GetStringFormatted(name, CultureInfo.CurrentUICulture.Name, arguments);
        }

        public LocalizedString GetStringFormatted(string name, string cultureName, params object[] arguments)
        {
            var localizedString = GetLocalizedString(name, cultureName);
            return new LocalizedString(name, string.Format(localizedString.Value, arguments), localizedString.ResourceNotFound, localizedString.SearchedLocation);
        }

        public LocalizedString GetLocalizedString(string name, string cultureName)
        {
            var value = Resource.Get()
                                .Get(cultureName)?.GetOrNull(name);

            if (value == null)
            {
                return new LocalizedString(name, name, resourceNotFound: true);
            }
            else
            {
                return value;
            }
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(
            string cultureName,
            bool includeParentCultures = true)
        {
            var allStrings = new Dictionary<string, LocalizedString>();

            if (includeParentCultures)
            {
                if (!string.IsNullOrWhiteSpace(Resource.DefaultCultureName))
                {
                    Resource.Get()
                            .Get(Resource.DefaultCultureName)?.Fill(allStrings);
                }

                if (cultureName.Contains("-"))
                {
                    Resource.Get()
                            .Get(Helper.GetBaseCultureName(cultureName))?.Fill(allStrings);
                }
            }

            Resource.Get()
                    .Get(cultureName)?.Fill(allStrings);

            return new List<LocalizedString>(allStrings.Values);
        }


        public class WithCultureStringLocalizer : IStringLocalizer
        {
            private readonly string _cultureName;
            private readonly JStringLocalizer _stringLocalizer;

            LocalizedString IStringLocalizer.this[string name] => _stringLocalizer.GetLocalizedString(name, _cultureName);

            LocalizedString IStringLocalizer.this[string name, params object[] arguments] => _stringLocalizer.GetStringFormatted(name, _cultureName, arguments);

            public WithCultureStringLocalizer(string cultureName, JStringLocalizer stringLocalizer)
            {
                _cultureName = cultureName;
                _stringLocalizer = stringLocalizer;
            }

            public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            {
                return _stringLocalizer.GetAllStrings(_cultureName, includeParentCultures);
            }

            public IStringLocalizer WithCulture(CultureInfo culture)
            {
                return new WithCultureStringLocalizer(culture.Name, _stringLocalizer);
            }
        }
    }
}
