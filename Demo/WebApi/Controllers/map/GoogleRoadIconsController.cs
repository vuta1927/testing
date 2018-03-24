using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Authorization;
using WebApi.Model;

namespace WebApi.Controllers.map
{
    [Produces("application/json")]
    [Route("api/GoogleRoadIcons")]
    [AppAuthorize(DemoPermissions.MapView)]
    public class GoogleRoadIconsController : Controller
    {
        private readonly DemoContext _context;

        public GoogleRoadIconsController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/GoogleRoadIcons
        [HttpGet]
        public IEnumerable<GoogleRoadIcon> GetGoogleRoadIcons()
        {
            return _context.GoogleRoadIcons;
        }

        // GET: api/GoogleRoadIcons/5
        [HttpGet("{id}")]
        [AppAuthorize(DemoPermissions.MapAdd, DemoPermissions.MapEdit)]
        public async Task<IActionResult> GetGoogleRoadIcon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var googleRoadIcon = await _context.GoogleRoadIcons.SingleOrDefaultAsync(m => m.Id == id);

            if (googleRoadIcon == null)
            {
                return NotFound();
            }

            return Ok(googleRoadIcon);
        }

        // PUT: api/GoogleRoadIcons/5
        [HttpPut("{id}")]
        [AppAuthorize(DemoPermissions.MapAdd, DemoPermissions.MapEdit)]
        public async Task<IActionResult> PutGoogleRoadIcon([FromRoute] int id, [FromBody] GoogleRoadIcon googleRoadIcon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != googleRoadIcon.Id)
            {
                return BadRequest();
            }

            _context.Entry(googleRoadIcon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoogleRoadIconExists(id))
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

        // POST: api/GoogleRoadIcons
        [HttpPost]
        [AppAuthorize(DemoPermissions.MapAdd, DemoPermissions.MapEdit)]
        public async Task<IActionResult> PostGoogleRoadIcon([FromBody] GoogleRoadIcon googleRoadIcon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var icon = new GoogleRoadIcon()
            {
                GoogleRoadId = googleRoadIcon.GoogleRoadId,
                Descriptions = googleRoadIcon.Descriptions,
                Lat = googleRoadIcon.Lat,
                Lng = googleRoadIcon.Lng,
                Location = googleRoadIcon.Location,
                Url = googleRoadIcon.Url
        };
            
            _context.GoogleRoadIcons.Add(icon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoogleRoadIcon", new { id = icon.Id }, icon);
        }

        // DELETE: api/GoogleRoadIcons/5
        [HttpDelete("{id}")]
        [AppAuthorize(DemoPermissions.MapDelete)]
        public async Task<IActionResult> DeleteGoogleRoadIcon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var googleRoadIcon = await _context.GoogleRoadIcons.SingleOrDefaultAsync(m => m.Id == id);
            if (googleRoadIcon == null)
            {
                return NotFound();
            }

            _context.GoogleRoadIcons.Remove(googleRoadIcon);
            await _context.SaveChangesAsync();

            return Ok(googleRoadIcon);
        }

        private bool GoogleRoadIconExists(int id)
        {
            return _context.GoogleRoadIcons.Any(e => e.Id == id);
        }
    }
}