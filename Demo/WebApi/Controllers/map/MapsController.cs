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
using IdentityModel.Client;
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
        public new ICollection<Coordinate> Paths { get; set; }

        public List<GoogleRoadIcon> GoogleRoadIcons { get; set; }
    }

    public class Coordinate
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class GMap: Map
    {
        public new ICollection<GRoad> GoogleRoads { get; set; }
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
            var caller = User as ClaimsPrincipal;
            List<int> roleIds = new List<int>();
            List<Map> maps = new List<Map>();
            foreach (var claim in caller.Claims)
            {
                if (claim.Type == "Roles")
                {
                    var role = _context.Roles.Where(r => r.RoleName == claim.Value).Select(i=>i.Id).FirstOrDefault();
                    roleIds.Add(role);
                }
            }
            foreach (var id in roleIds)
            {
                maps = _context.MapRoles.Where(mr => mr.RoleId == id).Select(i => i.Map).Include(m => m.CommentIcons).Include(com => com.GoogleRoads).ToList();
            }
            var Gmaps = new List<GMap>();
            foreach (var map in maps)
            {
                var newMap = new GMap();
                var newRoads = new List<GRoad>();
                foreach (var baseRoad in map.GoogleRoads)
                {
                    var icons = _context.GoogleRoadIcons.Where(g => g.GoogleRoadId == baseRoad.Id).ToList();
                    var newRoad = new GRoad()
                    {
                        Id = baseRoad.Id,
                        Color = baseRoad.Color,
                        Direction = baseRoad.Direction,
                        Distance = baseRoad.Distance,
                        Name = baseRoad.Name,
                        Paths = ConvertCoordinates(baseRoad.Paths),
                        GoogleRoadIcons = icons,
                        MapId = map.Id
                    };
                    newRoads.Add(newRoad);
                }
                newMap.GoogleRoads = newRoads;
                newMap.Id = map.Id;
                newMap.Type = map.Type;
                foreach (var component in map.CommentIcons)
                {
                    component.Map = null;
                }
                newMap.CommentIcons = map.CommentIcons;
                Gmaps.Add(newMap);
            }
            return Ok(Gmaps);
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
        [AppAuthorize(DemoPermissions.AddMap)]
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
        [AppAuthorize(DemoPermissions.DeleteMap)]
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
                if (!string.IsNullOrEmpty(t))
                {
                    var coord = new Coordinate();
                    var arrCoord = t.Split(',');
                    coord.Lat = Double.Parse(arrCoord[0]);
                    coord.Lng = Double.Parse(arrCoord[1]);
                    result.Add(coord);
                }
            }

            return result;
        }
    }

}