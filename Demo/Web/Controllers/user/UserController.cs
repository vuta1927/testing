using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Demo.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Web.Model;
using Web.Model.views;
namespace Web.Controllers.user
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : AppController
    {
        private readonly DemoContext _ctx;
        private readonly ILogger<UserController> _logger;

        public UserController(DemoContext context, ILogger<UserController> iLogger)
        {
            this._ctx = context;
            this._logger = iLogger;
        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            try
            {
                var users = _ctx.Users.ToList();
                var viewUsers = new List<UserView>();
                if (users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        viewUsers.Add(new UserView()
                        {
                            id = user.Id,
                            username = user.UserName,
                            email = user.Email,
                            firstname = user.Name,
                            lastname = user.Surname,
                            accessFailedCount = user.AccessFailedCount,
                            isLockoutEnabled = user.IsLockoutEnabled,
                            lockoutEndDateUtc = user.LockoutEndDateUtc,
                            isActive = user.IsActive
                        });
                    }
                    return Ok(viewUsers);
                }
                else
                    return BadRequest("Cant get any users!");
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }
    }
}