using System.Collections.Generic;
using System.IO;

namespace JLocalizer
{
    public interface IJLocalizationResourceDeserialize
    {
        IDictionary<string, string> Get(Stream stream);
    }
}
