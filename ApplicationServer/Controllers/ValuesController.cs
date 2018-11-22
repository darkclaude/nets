using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
namespace ApplicationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("asp")]
        public ActionResult<string> Get(int id)
        {
            return richsurvey();
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
        public string richsurvey()
        {
            var client = new RestClient("http://richsurvey.herokuapp.com");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("asp", Method.GET);
            request.AddQueryParameter("time", DateTime.Today.ToLongDateString());



            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            Console.WriteLine(content);
             return content;
        }
    }

}
