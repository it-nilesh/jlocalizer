using JLocalizer.Web.ApiResource;
using JLocalizer.Web.Db;
using JLocalizer.Web.TestLoc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;
using System.Globalization;

namespace JLocalizer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IStringLocalizer<EFLocalizationContext> _dbLocalizer;
        private readonly IStringLocalizer<ApiExternalLocalization> _apiLocalizer;
        private readonly IStringLocalizer<TestLocResource> _jsonLocalizer;

        public ValuesController(IStringLocalizer<EFLocalizationContext> dbLocalizer,
                                IStringLocalizer<ApiExternalLocalization> apiLocalizer,
                                IStringLocalizer<TestLocResource> jsonLocalizer)
        {
            _dbLocalizer = dbLocalizer;
            _apiLocalizer = apiLocalizer;
            _jsonLocalizer = jsonLocalizer;
        }

        // GET api/values
        [HttpGet]
        public Loc Get()
        {
            if (HttpContext.Request.Query.TryGetValue("lang", out StringValues vs))
            {
                CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(vs);
            }

            return new Loc
            {
                Api = _apiLocalizer["Hello"].Value,
                Db = _dbLocalizer["Hello"].Value,
                JsonFile = _jsonLocalizer["Hello"].Value,
            };
        }

        [HttpGet("Reset")]
        public void Reset()
        {
            _apiLocalizer.Reset();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Loc
    {
        public string Db { get; set; }

        public string Api { get; set; }

        public string JsonFile { get; set; }
    }
}
