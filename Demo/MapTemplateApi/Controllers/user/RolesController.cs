﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.Security;
using Demo.Security.Permissions;
using Microsoft.Azure.KeyVault.Models;
using DAL.Core.Authorization;
using DAL.Model;
using DAL.Model.views;
namespace MapTemplateApi.Controllers.user
{

    class RoleManage : Demo.Security.Role
    {
        public List<Permission> Permissions { get; set; }
    }
    [AppAuthorize(DemoPermissions.ViewRole)]
    [Produces("application/json")]
    [Route("api/Roles")]
    public class RolesController : Controller
    {
        private readonly MapTemplateContext _context;

        public RolesController(MapTemplateContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public IActionResult GetRole()
        {
            var roles = new List<RoleModel.RoleBase>();
            foreach (var r in _context.Role)
            {
                var role = new RoleModel.RoleBase()
                {
                    Id = r.Id,
                    RoleName = r.RoleName,
                    Descriptions = r.Descriptions
                };
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
            
            var roleForView = new RoleModel.RoleBase()
            {
                Id = role.Id,
                RoleName = role.RoleName,
                Descriptions = role.Descriptions,
            };

            return Ok(roleForView);

            //try
            //{
            //    var roleForView = new RoleModel.RoleForView()
            //    {
            //        Id = role.Id,
            //        RoleName = role.RoleName,
            //        Descriptions = role.Descriptions,
            //        Permissions = new List<PermissionModel.PermissionWithCategor>()
            //    };
            //    foreach (var row in _context.PermissionRoles.Where(p => p.RoleId == role.Id))
            //    {
            //        var data = _context.Permissions.Where(p => p.Id == row.PermissionId);
            //        foreach (var permission in data)
            //        {
            //            var viewPermission = new PermissionModel.PermissionWithCategor()
            //            {
            //                Id = permission.Id,
            //                Name = permission.Name,
            //                DisplayName = permission.DisplayName,
            //                Category = permission.Category,
            //                Descriptions = permission.Description,
            //                IsCheck = false
            //            };
            //            roleForView.Permissions.Add(viewPermission);
            //        }
            //    }

            //    return Ok(roleForView);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);

            //}
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole([FromRoute] int id, [FromBody] RoleModel.RoleForUpdate role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Id)
            {
                return BadRequest();
            }
            var originRole = _context.Role.SingleOrDefault(a => a.Id == id);
            if (originRole != null)
            {
                originRole.RoleName = role.RoleName;
                originRole.Descriptions = role.Descriptions;
                originRole.LastModifierUserId = role.LastModifierUserId;
                originRole.LastModificationTime = DateTime.Now;
            }
            //_context.Entry(role).State = EntityState.Modified;

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

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] RoleModel.RoleForCreate role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (RoleNameExists(role.RoleName))
            {
                return Ok(false);
            }
            var newRole = new Role()
            {
                RoleName = role.RoleName,
                Descriptions = role.Descriptions,
                IsDeleted = false,
                CreationTime = DateTime.Now,
                CreatorUserId = role.CreatorUserId,
                NormalizedRoleName = role.RoleName.ToUpper()
            };
            _context.Role.Add(newRole);
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

        private bool RoleNameExists(string name)
        {
            return _context.Role.Any(e => e.RoleName.ToUpper().Equals(name));
        }
    }
}