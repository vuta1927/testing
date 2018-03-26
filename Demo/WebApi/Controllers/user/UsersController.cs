using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.Security;
using Microsoft.AspNetCore.Identity;
using WebApi.Core.Authorization;
using WebApi.Model;
using WebApi.Model.views;

namespace WebApi.Controllers.user
{

    [AppAuthorize(DemoPermissions.ViewUser)]
    [Produces("application/json")]
    [Route("api/Users/[action]")]
    public class UsersController : Controller
    {
        private readonly DemoContext _context;
        private readonly UserManager<User> _userManager;
        public UsersController(DemoContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Users
        [HttpGet("{skip}/{take}")]
        public IEnumerable<User> GetUser(int skip, int take)
        {
            return _context.User.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserForCreateOrEdit([FromRoute] int id)
        {
            var userEdit = new UserModel.UserEdit();
            var result = new UserModel.UserForCreateOrEdit()
            {
                IsEditMode = true,
            };
            var userRoles = _context.UserRoles;
            var allRole = _context.Role;
            result.Roles = new List<UserModel.UserRole>();
            foreach (var role in allRole)
            {
                result.Roles.Add(new UserModel.UserRole()
                {
                    RoleId = role.Id,
                    RoleName = role.RoleName,
                    RoleDisplayName = role.RoleName,
                    IsAssigned = false
                });
            }

            

            if (id > 0)
            {
                var currentUser = _context.User.SingleOrDefault(u => u.Id == id);

                if (currentUser != null)
                {
                    userEdit.Id = currentUser.Id;
                    userEdit.Name = currentUser.Name;
                    userEdit.Surname = currentUser.Surname;
                    userEdit.Username = currentUser.UserName;
                    userEdit.EmailAddress = currentUser.Email;
                    userEdit.Password = "";
                    userEdit.IsActive = currentUser.IsActive;
                    userEdit.ShouldChangePasswordOnNextLogin = false;

                    result.User = userEdit;
                }
                else
                {

                    return NotFound();
                }

                foreach (var role in result.Roles)
                {
                    var data = userRoles.SingleOrDefault(a => a.RoleId == role.RoleId && a.UserId == id);
                    if (data != null)
                    {
                        role.IsAssigned = true;
                    }
                }
            }
            else
            {
                userEdit.Name = "";
                userEdit.Surname = "";
                userEdit.Username = "";
                userEdit.EmailAddress = "";
                userEdit.Password = "";
                userEdit.IsActive = false;
                userEdit.ShouldChangePasswordOnNextLogin = false;

                result.User = userEdit;
                result.IsEditMode = false;
            }
            return Ok(result);
        }

        [HttpGet("{email}")]
        public IActionResult WithEmail([FromRoute] string email)
        {
            var user = _context.User.SingleOrDefault(u=>u.Email == email);
            if (user != null)
                return Ok(user);
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{username}")]
        public IActionResult WithUserName([FromRoute] string username)
        {
            var user = _context.User.SingleOrDefault(u => u.UserName == username);
            if (user != null)
                return Ok(user);
            else
            {
                return NotFound();
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] long id, [FromBody] UserModel.UserForCreate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            var originUser = _context.User.SingleOrDefault(u => u.Id == id);
            if (originUser == null) return NotFound();

            originUser.UserName = user.Username;
            originUser.NormalizedEmail = user.EmailAddress.ToUpper();
            originUser.Name = user.Name;
            originUser.IsActive = user.IsActive;
            originUser.Surname = user.Surname;
            originUser.NormalizedUserName = user.Username.ToUpper();
            originUser.Email = user.EmailAddress;
            originUser.EmailConfirmed = user.SendActivationEmail;

            if (!string.IsNullOrEmpty(user.Password))
            {
                var passwd = _userManager.PasswordHasher.HashPassword(originUser, user.Password);
                originUser.PasswordHash = passwd;
            }

            try
            {
                await _context.SaveChangesAsync();
                var currentUserRoles = _context.UserRoles.Where(r => r.UserId == user.Id);

                _context.UserRoles.RemoveRange(currentUserRoles);

                foreach (var roleName in user.AssignedRoleNames)
                {
                    var role = _context.Role.SingleOrDefault(r => r.RoleName == roleName);
                    if (role != null)
                    {
                        await _context.UserRoles.AddAsync(new UserRole(originUser.Id, role.Id));
                    }

                }
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new { id = originUser.Id }, originUser);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserModel.UserForCreate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.User.Any(e => e.UserName == user.Username))
            {
                return Ok(false);
            }

            var newUser = new User()
            {
                UserName = user.Username,
                NormalizedEmail = user.EmailAddress.ToUpper(),
                Name = user.Name,
                IsActive = user.IsActive,
                Surname = user.Surname,
                NormalizedUserName = user.Username.ToUpper(),
                Email = user.EmailAddress,
                EmailConfirmed = user.SendActivationEmail
            };
            var passwd = _userManager.PasswordHasher.HashPassword(newUser, user.Password);
            newUser.PasswordHash = passwd;

            try
            {
                await _context.User.AddAsync(newUser);
                foreach (var roleName in user.AssignedRoleNames)
                {
                    var role = _context.Role.SingleOrDefault(r => r.RoleName == roleName);
                    if (role != null)
                    {
                        await _context.UserRoles.AddAsync(new UserRole(newUser.Id, role.Id));
                    }

                }
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new { id = newUser.Id }, newUser);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            //_context.User.Add(user);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(long id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}