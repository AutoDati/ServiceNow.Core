using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo_WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly HttpClient _httpClient;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public User GetLoggedUserDetails()
        {
            _logger.LogDebug(User.Identity.Name);
            _logger.LogDebug($"isLogged? {User.Identity.IsAuthenticated}");
            if (User.Identity.IsAuthenticated)
                return new User()
                {
                    Id = new Guid(User.Claims.First(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value),
                    Name = User.Claims.First(c => c.Type == "name").Value,
                    Email = User.Claims.First(c => c.Type == "preferred_username").Value
                };

            return null;
        }

        //[HttpPost]
        //public string login()
        //{
        //    _httpClient.PostAsync()
        //}
    }
}
