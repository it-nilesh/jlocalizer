using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JLocalizer.Resource.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet("{lang}")]
        public Dictionary<string, string> Get(string lang)
        {
            var dics = new Dictionary<string, string>();

            if (lang == "en-IN")
            {
                dics.Add("Hello", "Api en-IN- "+ Guid.NewGuid());
            }
            else if (lang == "en-US")
            {
                dics.Add("Hello", "Api en-US- " + Guid.NewGuid());
            }

            return dics;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
}
