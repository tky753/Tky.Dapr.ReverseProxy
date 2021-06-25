using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceSample.Controllers
{
    [Controller]
    [Route("sample")]
    public class SampleController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SampleController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{account}")]
        public ActionResult Get(string account)
        {
            return new JsonResult(new
            {
                Account = account,
                Balance = 100
            });
        }

        [HttpGet("req")]
        public ActionResult GetReq()
        {
            var req = _httpContextAccessor.HttpContext!.Request;
            return new JsonResult(new
            {
                Headers = req.Headers,
            });
        }
    }
}