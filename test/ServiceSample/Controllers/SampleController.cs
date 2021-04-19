using Microsoft.AspNetCore.Mvc;

namespace ServiceSample.Controllers
{
    [Controller]
    [Route("sample")]
    public class SampleController
    {
        [HttpGet("{account}")]
        public ActionResult Get(string account)
        {
            return new JsonResult(new
            {
                Account = account,
                Balance = 100
            });
        }
    }
}