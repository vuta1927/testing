using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.Security;
using Demo.Security.Permissions;
using WebApi.Core.Authorization;
using WebApi.Model;

namespace WebApi.Controllers.user
{

    class RoleManage : Role
    {
        public List<Permission> Permissions { get; set; }
    }

    [Produces("application/json")]
    [Route("api/Roles")]
    public class RolesController : Controller
    {
        private readonly DemoContext _context;

        public RolesController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public IActionResult GetRole()
        {
            var roles = new List<RoleManage>();
            foreach (var r in _context.Role)
            {
                var role = new RoleManage()
                {
                    Id = r.Id,
                    RoleName = r.RoleName,
                    NormalizedRoleName = r.NormalizedRoleName,
                    RoleClaims = r.RoleClaims,
                    CreationTime = r.CreationTime,
                    CreatorUser = r.CreatorUser,
                    CreatorUserId = r.CreatorUserId,
                    DeleterUser = r.DeleterUser,
                    DeleterUserId = r.DeleterUserId,
                    DeletionTime = r.DeletionTime,
                    Descriptions = r.Descriptions,
                    IsDeleted = r.IsDeleted,
                    LastModificationTime = r.LastModificationTime,
                    LastModifierUser = r.LastModifierUser,
                    LastModifierUserId = r.LastModifierUserId,
                    Permissions = new List<Permission>()
                };
                foreach (var row in _context.PermissionRoles.Where(p=>p.RoleId == role.Id))
                {
                    role.Permissions.AddRange(_context.Permissions.Where(p => p.Id == row.PermissionId));
                }
                roles.Add(role);
            }
            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _context.Role.SingleOrDefaultAsync(m => m.Id == id);
            
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole([FromRoute] int id, [FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _context.Role.SingleOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Role.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(role);
        }

        private bool RoleExists(int id)
        {
            return _context.Role.Any(e => e.Id == id);
        }
    }
}