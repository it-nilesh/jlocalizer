using Microsoft.Extensions.Localization;
using System;

namespace JLocalizer
{
    public abstract class LocalizerFactory<T> : IStringLocalizerFactory
    {
        protected Type Resource => typeof(T);

        public IStringLocalizer Create(Type resourceSource)
        {
            return CreateLocalizerIntance(resourceSource);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return CreateLocalizerIntance(baseName, location);
        }
        
        public abstract StringLocalizerAdapter CreateLocalizerIntance(Type resourceSource);

        public abstract StringLocalizerAdapter CreateLocalizerIntance(string baseName, string location);
    }
}