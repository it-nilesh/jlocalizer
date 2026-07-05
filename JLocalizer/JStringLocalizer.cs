using Microsoft.Extensions.Localization;
using System;
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
            return new LocalizedString(
                name,
                string.Format(CultureInfo.CurrentCulture, localizedString.Value, arguments),
                localizedString.ResourceNotFound,
                localizedString.SearchedLocation);
        }

        public LocalizedString GetLocalizedString(string name, string cultureName)
        {
            var stores = Resource.Get();

            if (TryGetLocalizedString(stores, cultureName, name, out var value))
            {
                return value;
            }

            var parentCulture = cultureName;
            while (!string.IsNullOrWhiteSpace(parentCulture) && parentCulture.IndexOf('-') >= 0)
            {
                parentCulture = Helper.GetBaseCultureName(parentCulture);
                if (TryGetLocalizedString(stores, parentCulture, name, out value))
                {
                    return value;
                }
            }

            if (!string.IsNullOrWhiteSpace(Resource.DefaultCultureName) &&
                !string.Equals(Resource.DefaultCultureName, cultureName, StringComparison.OrdinalIgnoreCase) &&
                TryGetLocalizedString(stores, Resource.DefaultCultureName, name, out value))
            {
                return value;
            }

            return new LocalizedString(name, name, resourceNotFound: true);
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(
            string cultureName,
            bool includeParentCultures = true)
        {
            var allStrings = new Dictionary<string, LocalizedString>();
            var stores = Resource.Get();

            if (includeParentCultures)
            {
                if (!string.IsNullOrWhiteSpace(Resource.DefaultCultureName))
                {
                    stores.Get(Resource.DefaultCultureName)?.Fill(allStrings);
                }

                var parentCultures = GetParentCultures(cultureName);
                for (var index = parentCultures.Count - 1; index >= 0; index--)
                {
                    stores.Get(parentCultures[index])?.Fill(allStrings);
                }
            }

            stores.Get(cultureName)?.Fill(allStrings);

            return new List<LocalizedString>(allStrings.Values);
        }

        private static bool TryGetLocalizedString(
            IReadOnlyDictionary<string, IJLocalizationStore> stores,
            string cultureName,
            string name,
            out LocalizedString localizedString)
        {
            localizedString = null;

            if (string.IsNullOrWhiteSpace(cultureName))
                return false;

            var store = stores.Get(cultureName);
            if (store == null)
                return false;

            localizedString = store.GetOrNull(name);
            return localizedString != null;
        }

        private static List<string> GetParentCultures(string cultureName)
        {
            var cultures = new List<string>();

            while (!string.IsNullOrWhiteSpace(cultureName) && cultureName.IndexOf('-') >= 0)
            {
                cultureName = Helper.GetBaseCultureName(cultureName);
                if (!string.IsNullOrWhiteSpace(cultureName))
                    cultures.Add(cultureName);
            }

            return cultures;
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
