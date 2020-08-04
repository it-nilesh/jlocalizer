using Microsoft.Extensions.Localization;
using System;

namespace JLocalizer
{
    public interface IExternalLocalizerFactory
    {
        IStringLocalizer Create(Type resource);

        IStringLocalizer Create(string baseName, string location);
    }
}
