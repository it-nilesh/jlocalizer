using Http.Consumer;
using System;
using System.Collections.Generic;

namespace JLocalizer.Web.ApiResource
{
    public class ApiExternalLocalization : JLocalizationResourceBinder
    {
        private readonly IHttpConsumer _httpConsumer;

        public ApiExternalLocalization(IHttpConsumer httpConsumer)
        {
            _httpConsumer = httpConsumer.Host("http://localhost:3948/api/");
        }

        public override IReadOnlyDictionary<string, IJLocalizationStore> Execute()
        {
            var localizedStore = new Dictionary<string, IJLocalizationStore>(StringComparer.OrdinalIgnoreCase);

            var resources = _httpConsumer.Resource("Values/en-IN")
                                        .Get<Dictionary<string, string>>()
                                        .BuildAsync()
                                        .GetAwaiter()
                                        .GetResult();
            localizedStore.AddJLocalization("en-IN", resources);

            resources = _httpConsumer.Resource("Values/en-US")
                                        .Get<Dictionary<string, string>>()
                                        .BuildAsync()
                                        .GetAwaiter()
                                        .GetResult();

            localizedStore.AddJLocalization("en-US", resources);
            return localizedStore;
        }
    }
}
