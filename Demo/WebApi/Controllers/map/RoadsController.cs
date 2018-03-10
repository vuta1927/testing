using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace Web.Controllers.road
{
    [Produces("application/json")]
    [Route("api/Roads")]
    public class RoadsController : Controller
    {
        private readonly DemoContext _context;

        public RoadsController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/Roads
        [HttpGet]
        public IEnumerable<GoogleRoad> GetRoads()
        {
            return _context.GoogleRoads;
        }

        // GET: api/Roads/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var road = await _context.GoogleRoads.SingleOrDefaultAsync(m => m.Id == id);

            if (road == null)
            {
                return NotFound();
            }

            return Ok(road);
        }

        // PUT: api/Roads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoad([FromRoute] int id, [FromBody] GoogleRoad road)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != road.Id)
            {
                return BadRequest();
            }

            _context.Entry(road).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoadExists(id))
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

        // POST: api/Roads
        [HttpPost]
        public async Task<IActionResult> PostRoad([FromBody] GoogleRoad road)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GoogleRoads.Add(road);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoad", new { id = road.Id }, road);
        }

        // DELETE: api/Roads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var road = await _context.GoogleRoads.SingleOrDefaultAsync(m => m.Id == id);
            if (road == null)
            {
                return NotFound();
            }

            _context.GoogleRoads.Remove(road);
            await _context.SaveChangesAsync();

            return Ok(road);
        }

        private bool RoadExists(int id)
        {
            return _context.GoogleRoads.Any(e => e.Id == id);
        }
    }
}