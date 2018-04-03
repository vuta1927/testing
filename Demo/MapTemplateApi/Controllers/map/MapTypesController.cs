using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Model;

namespace MapTemplateApi.Controllers.map
{
    [Produces("application/json")]
    [Route("api/MapTypes")]
    public class MapTypesController : Controller
    {
        private readonly MapTemplateContext _context;

        public MapTypesController(MapTemplateContext context)
        {
            _context = context;
        }

        // GET: api/MapTypes
        [HttpGet]
        public IEnumerable<MapType> GetMapTypes()
        {
            return _context.MapTypes;
        }

        // GET: api/MapTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMapType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapType = await _context.MapTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (mapType == null)
            {
                return NotFound();
            }

            return Ok(mapType);
        }

        // PUT: api/MapTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMapType([FromRoute] int id, [FromBody] MapType mapType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mapType.Id)
            {
                return BadRequest();
            }

            _context.Entry(mapType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapTypeExists(id))
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

        // POST: api/MapTypes
        [HttpPost]
        public async Task<IActionResult> PostMapType([FromBody] MapType mapType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MapTypes.Add(mapType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MapTypeExists(mapType.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMapType", new { id = mapType.Id }, mapType);
        }

        // DELETE: api/MapTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapType = await _context.MapTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (mapType == null)
            {
                return NotFound();
            }

            _context.MapTypes.Remove(mapType);
            await _context.SaveChangesAsync();

            return Ok(mapType);
        }

        private bool MapTypeExists(int id)
        {
            return _context.MapTypes.Any(e => e.Id == id);
        }
    }
}