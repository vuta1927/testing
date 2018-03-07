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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApi.Core.Authorization;
using WebApi.Model;

namespace WebApi.Controllers.map
{
    [Produces("application/json")]
    [Route("api/Maps")]
    [AppAuthorize(DemoPermissions.ViewMap)]
    public class MapsController : Controller
    {
        private readonly DemoContext _context;

        public MapsController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/Maps
        [HttpGet]
        public IEnumerable<Map> GetMaps()
        {
            var email = User.FindFirst("sub")?.Value;

            return _context.Maps;
        }

        // GET: api/Maps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMap([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var map = await _context.Maps.SingleOrDefaultAsync(m => m.Id == id);

            if (map == null)
            {
                return NotFound();
            }

            return Ok(map);
        }

        // PUT: api/Maps/5
        [HttpPut("{id}")]
        [AppAuthorize(DemoPermissions.EditMap)]
        public async Task<IActionResult> PutMap([FromRoute] int id, [FromBody] Map map)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != map.Id)
            {
                return BadRequest();
            }

            _context.Entry(map).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapExists(id))
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

        // POST: api/Maps
        [HttpPost]
        [AppAuthorize(DemoPermissions.EditMap)]
        public async Task<IActionResult> PostMap([FromBody] Map map)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Maps.Add(map);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMap", new { id = map.Id }, map);
        }

        // DELETE: api/Maps/5
        [HttpDelete("{id}")]
        [AppAuthorize(DemoPermissions.EditMap)]
        public async Task<IActionResult> DeleteMap([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var map = await _context.Maps.SingleOrDefaultAsync(m => m.Id == id);
            if (map == null)
            {
                return NotFound();
            }

            _context.Maps.Remove(map);
            await _context.SaveChangesAsync();

            return Ok(map);
        }

        private bool MapExists(int id)
        {
            return _context.Maps.Any(e => e.Id == id);
        }
    }
}