using System;
using System.Collections.Generic;
using System.Data;
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
using  WebApi.Util;

namespace WebApi.Controllers.map
{
    public class GRoad: GoogleRoad
    {
        public int MapId { get; set; }
        public new Direction Direction { get; set; }
        public new ICollection<Coordinate> Paths { get; set; }

    }

    public class Direction
    {
        public string Value { get; set; }
        public string Display { get; set; }
    }

    public class Coordinate
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class GMapConponent: MapComponent
    {
        public new ICollection<GRoad> Roads { get; set; }
    }


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
        public IActionResult GetMaps()
        {
            //var email = User.FindFirst("sub")?.Value;
            var result = new List<Map>();
            var data = _context.Maps.Include(map=>map.MapComponent).ThenInclude(com=>com.Roads);
            foreach (var map in data)
            {
                var newComponent = new GMapConponent();
                var newRoads = new List<GRoad>();
                foreach (var baseRoad in map.MapComponent.Roads)
                {
                    var newRoad = new GRoad()
                    {
                        Id = baseRoad.Id,
                        Color = baseRoad.Color,
                        Direction = new Direction(){Display = baseRoad.Direction, Value = Util.MapUtilities.ConvertToUnSign(baseRoad.Direction)},
                        Distance = baseRoad.Distance,
                        Name = baseRoad.Name,
                        Paths = ConvertCoordinates(baseRoad.Paths),
                        MapId = map.Id
                    };
                    newRoads.Add(newRoad);
                }
                newComponent.Id = map.MapComponent.Id;
                newComponent.Roads = newRoads;
                map.MapComponent = newComponent;
                //result.Add(newMap);
            }
            return Ok(data);
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

        private ICollection<Coordinate> ConvertCoordinates(string paths)
        {
            var result = new List<Coordinate>();
            var temp = paths.Split(';');
            foreach (var t in temp)
            {
                var coord = new Coordinate();
                var arrCoord = t.Split(',');
                coord.Lat = Double.Parse(arrCoord[0]);
                coord.Lng = Double.Parse(arrCoord[1]);
                result.Add(coord);
            }

            return result;
        }
    }

}