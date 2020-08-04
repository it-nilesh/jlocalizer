using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace JLocalizer.Web.Db
{
    public class DbStringLocalizer : StringLocalizerAdapter
    {
        private readonly EFLocalizationContext _eFLocalization;
        public DbStringLocalizer(EFLocalizationContext eFLocalization)
        {
            _eFLocalization = eFLocalization;
        }

        public override IReadOnlyList<LocalizedString> GetAllStrings(string cultureName, bool includeParentCultures = true)
        {
            //TODO: 
            return default;
        }

        public override IStringLocalizer WithCulture<T>(CultureInfo culture, T stringLocalizer)
        {
            //TODO: 
            return default;
        }

        public override LocalizedString GetLocalizedString(string name, string cultureName)
        {
            var lang = _eFLocalization.Langs.Where(x => x.Lang == cultureName).SelectMany(x => x.Data)
                  .FirstOrDefault(x => x.Key == name);

            return new LocalizedString(lang.Name,lang.Name);
        }

        public override LocalizedString GetStringFormatted(string name, string cultureName, params object[] arguments)
        {
            //TODO: 
            return default;
        }
    }
}
