using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.Controllers.map
{
    public class RequestRoad
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Paths { get; set; }
        public string Color { get; set; }
        public double Distance { get; set; }
        public string Direction { get; set; }
        public int MapId { get; set; }
    }

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
        public async Task<IActionResult> PutRoads([FromRoute] int id, [FromBody] List<RequestRoad> data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id > 0)
            {
                var road = data[0];
                if (id != road.Id)
                {
                    return BadRequest();
                }

                var mapComponentId = _context.MapComponents.Where(c => c.MapId == road.MapId).Select(m => m.Id).FirstOrDefault();
                var r = new GoogleRoad()
                {
                    Id = road.Id,
                    Name = road.Name,
                    Direction = road.Direction,
                    Distance = road.Distance,
                    Color = road.Color,
                    Paths = road.Paths,
                    MapComponentId = mapComponentId
                };

                _context.Entry(r).State = EntityState.Modified;

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
            }
            else if (id == -1)
            {
                for (var i = 0; i < data.Count; i++)
                {
                    var mapComponentId = _context.MapComponents.Where(c => c.MapId == data[i].MapId).Select(m => m.Id).FirstOrDefault();
                    var road = new GoogleRoad()
                    {
                        Id = data[i].Id,
                        Name = data[i].Name,
                        Direction = data[i].Direction,
                        Distance = data[i].Distance,
                        Color = data[i].Color,
                        Paths = data[i].Paths,
                        MapComponentId = mapComponentId
                    };

                    _context.Entry(road).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RoadExists(data[i].Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            return Ok("OK");
        }

        // POST: api/Roads
        [HttpPost]
        public async Task<IActionResult> PostRoad([FromBody] RequestRoad road)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mapComponent = _context.MapComponents.Where(c => c.MapId == road.MapId).FirstOrDefault();
            if (mapComponent != null)
            {
                var newRoad = new GoogleRoad()
                {
                    Paths = road.Paths,
                    Direction = road.Direction,
                    Distance = road.Distance,
                    Color = road.Color,
                    Name = road.Name,
                    MapComponentId = mapComponent.Id
                };
                _context.GoogleRoads.Add(newRoad);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRoad", new { id = newRoad.Id }, newRoad);
            }
            return BadRequest("There are no map component for this map.");
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