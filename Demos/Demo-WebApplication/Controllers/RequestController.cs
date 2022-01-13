using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Models;
using SNow.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo_WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IServiceNow _serviceNow;

        public RequestController(ILogger<UserController> logger, IServiceNow serviceNow)
        {
            _logger = logger;
            _serviceNow = serviceNow;
        }

        [AuthorizeForScopes(ScopeKeySection = "your Scope Like Impersonation... ")]
        [HttpGet]
        public async Task<List<User>> GetSomeRquests()
        {
            var requestTable = _serviceNow
                    .UsingTable<User>("sc_request")
                    .Limit(4);
            var requests = await requestTable.ToListAsync();

            return requests;

        }
    }
}
