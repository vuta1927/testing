﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.AspNetCore.Mvc.Security.Permissions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.Security.Permissions;
using WebApi.Model;
using WebApi.Model.views;

namespace WebApi.Controllers.user
{
    [Produces("application/json")]
    [Route("api/Permissions/[action]")]
    public class PermissionsController : Controller
    {
        private readonly DemoContext _context;

        public PermissionsController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/Permissions
        [HttpGet]
        public IEnumerable<Permission> GetPermission()
        {
            return _context.Permission;
        }

        // GET: api/Permissions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermission([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var permission = await _context.Permission.SingleOrDefaultAsync(m => m.Id == id);

            if (permission == null)
            {
                return NotFound();
            }

            return Ok(permission);
        }

        [HttpGet]
        public IActionResult GetCategorys()
        {
            var temp = _context.Permission.GroupBy(p => p.Category).Select(c=>c.Key);
            return Ok(temp);
        }

        [HttpGet("{roleId}")]
        public IActionResult GetPermissionByCategory([FromRoute] int roleId)
        {
            var categories = _context.Permission.GroupBy(p => p.Category).Select(c => c.Key);
            var permissionWithCategors = new List<PermissionModel.PermissionWithCategor>();
            foreach (var category in categories)
            {
                var permissions = _context.Permission.Where(p => p.Category == category).ToList();
                foreach (var permission in permissions)
                {
                    var p = new PermissionModel.PermissionWithCategor()
                    {
                        Id = permission.Id,
                        Name = permission.Name,
                        DisplayName = permission.DisplayName,
                        Descriptions = permission.Description,
                        Category = permission.Category,
                        IsCheck = false
                    };
                    permissionWithCategors.Add(p);
                }

                foreach (var permission in permissionWithCategors)
                {
                    var permisionRole = _context.PermissionRoles.SingleOrDefault(p => p.RoleId == roleId && p.PermissionId == permission.Id);
                    if (permisionRole != null)
                    {
                        permission.IsCheck = true;
                    }
                }
            }
            

            return Ok(permissionWithCategors);
        }

        // PUT: api/Permissions/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPermission([FromRoute] int id, [FromBody] Permission permission)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != permission.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(permission).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PermissionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Permissions
        //[HttpPost]
        //public async Task<IActionResult> PostPermission([FromBody] Permission permission)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Permission.Add(permission);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPermission", new { id = permission.Id }, permission);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> AddOrUpdatePermission([FromRoute] int id, [FromBody] PermissionModel.PermissionWithCategor permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (PermissionRoleExists(permission.Id, id))
            {
                var per = await _context.PermissionRoles.SingleOrDefaultAsync(m => m.PermissionId == permission.Id && m.RoleId == id);
                if (per == null)
                {
                    return NotFound();

                }
                _context.PermissionRoles.Remove(per);   
            }
            else
            {
                _context.PermissionRoles.Add(new PermissionRole() { PermissionId = permission.Id, RoleId = id});
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        // DELETE: api/Permissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var permission = await _context.Permission.SingleOrDefaultAsync(m => m.Id == id);
            if (permission == null)
            {
                return NotFound();
            }

            _context.Permission.Remove(permission);
            await _context.SaveChangesAsync();

            return Ok(permission);
        }

        private bool PermissionExists(int id)
        {
            return _context.Permission.Any(e => e.Id == id);
        }

        private bool PermissionRoleExists(int id, int roleId)
        {
            return _context.PermissionRoles.Any(e => e.PermissionId == id && e.RoleId == roleId);
        }
    }
}