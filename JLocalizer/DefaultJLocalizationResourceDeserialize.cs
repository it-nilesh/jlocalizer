using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace JLocalizer
{
    public class DefaultJLocalizationResourceDeserialize : IJLocalizationResourceDeserialize
    {
        public IDictionary<string, string> Get(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                JsonSerializer ser = new JsonSerializer();
                return ser.Deserialize<Dictionary<string, string>>(jsonReader);
            }
        }
    }
}
