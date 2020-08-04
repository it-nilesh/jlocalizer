using System;

namespace JLocalizer.Web.Db
{
    public class DbLocalizationFactory : LocalizerFactory<EFLocalizationContext>
    {
        private readonly EFLocalizationContext _eFLocalization;
        public DbLocalizationFactory(EFLocalizationContext eFLocalization)
        {
            _eFLocalization = eFLocalization;
        }

        public override StringLocalizerAdapter CreateLocalizerIntance(Type resourceSource)
        {
            return new DbStringLocalizer(_eFLocalization);
        }

        public override StringLocalizerAdapter CreateLocalizerIntance(string baseName, string location)
        {
            //TODO: 
            throw new NotImplementedException();
        }
    }
}
