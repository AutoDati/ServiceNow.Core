using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Models;
using SNow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_WebApplication.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class ServiceNowController : ControllerBase
    {   
        private readonly ILogger<UserController> _logger;
        private readonly IServiceNow _serviceNow;

        public ServiceNowController(ILogger<UserController> logger, IServiceNow serviceNow)
        {
            _logger = logger;
            _serviceNow = serviceNow;
        }

        //each setup would have a different one
        [AuthorizeForScopes(ScopeKeySection = "your scope")]
        [HttpGet]
        public async Task<List<User>> GetSomeUsers()
        {
            var usersTable = _serviceNow
                    .UsingTable<User>("sys_user")
                    .Limit(4)
                    .WithQuery(x => $"{x.Name} like Branco and {x.Country} = BR");
            var users = await usersTable.ToListAsync();       

            return users;

        }
    }
}
