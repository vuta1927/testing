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
using DAL.Core.Authorization;
using DAL.Model;
using DAL.Model.views;

namespace MapTemplateApi.Controllers.map
{
    public class GRoad: GoogleRoad
    {
        public new int MapId { get; set; }
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
        public string TypeName { get; set; }
        public int Type { get; set; }
        public new ICollection<GRoad> GoogleRoads { get; set; }
    }


    [Produces("application/json")]
    [Route("api/Maps/[action]")]
    [AppAuthorize(DemoPermissions.ViewMap)]
    public class MapsController : Controller
    {
        private readonly MapTemplateContext _context;

        public MapsController(MapTemplateContext context)
        {
            _context = context;
        }

        // GET: api/Maps
        [HttpGet]
        public IActionResult GetMapsByRole()
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
                maps = _context.MapRoles.Where(mr => mr.RoleId == id).Select(i => i.Map).Include(m => m.CommentIcons).Include(com => com.GoogleRoads).Include(x=>x.MapType).ToList();
            }
            var gmaps = new List<GMap>();
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
                newMap.Name = map.Name;
                newMap.Type = map.MapTypeId;
                newMap.Descriptions = map.Descriptions;
                newMap.TypeName = map.MapType.Name;
                foreach (var component in map.CommentIcons)
                {
                    component.Map = null;
                }
                newMap.CommentIcons = map.CommentIcons;
                gmaps.Add(newMap);
            }
            return Ok(gmaps);
        }

        [HttpGet("{id}")]
        public IActionResult GetMaps([FromRoute] int id)
        {
            var result = new List<MapModel.MapView>();
            var maps = new List<Map>();
            if (id < 0)
            {
                maps = _context.Maps.Include(x => x.MapType).ToList();
            }
            else
            {
                maps = _context.Maps.Include(x => x.MapType).ToList();
            }
            var roles = _context.Role;
            foreach (var map in maps)
            {
                var newMap = new MapModel.MapView
                {
                    Id = map.Id,
                    Descriptions = map.Descriptions,
                    Name = map.Name,
                    Roles = new List<MapModel.RoleMap>(),
                    TypeName = map.MapType.Name,
                    Type = map.MapType
                };
                foreach (var role in roles)
                {
                    var newMapRole = new MapModel.RoleMap
                    {
                        RoleName = role.RoleName,
                        IsAssigned = false,
                        RoleDisplayName = role.RoleName,
                        RoleId = role.Id,
                    };
                    if (_context.MapRoles.Any(x => x.RoleId == role.Id && x.MapId == map.Id))
                    {
                        newMapRole.IsAssigned = true;
                    }
                    newMap.Roles.Add(newMapRole);
                }
                result.Add(newMap);
            }
            
            return Ok(result);
        }

        // PUT: api/Maps/5
        [HttpPut("{id}")]
        [AppAuthorize(DemoPermissions.EditMap)]
        public async Task<IActionResult> PutMap([FromRoute] int id, [FromBody] MapModel.MapUpdateEdit map)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != map.Id)
            {
                return BadRequest();
            }

            var originMap = _context.Maps.SingleOrDefault(x => x.Id == map.Id);
            if (originMap != null)
            {
                originMap.Name = map.Name;
                originMap.Descriptions = map.Descriptions;
                originMap.MapTypeId = map.Type;
            }

            try
            {
                await _context.SaveChangesAsync();

                var mapRoles = _context.MapRoles.Where(x=>x.MapId == id);

                _context.MapRoles.RemoveRange(mapRoles);

                await _context.SaveChangesAsync();

                foreach (var roleName in map.RolesAssigned)
                {
                    var role = _context.Role.SingleOrDefault(x => x.RoleName == roleName);
                    if (role != null)
                    {
                        _context.MapRoles.Add(new MapRole { MapId = id, RoleId = role.Id });

                        await _context.SaveChangesAsync();
                    }
                }
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

            return Ok("OK");
        }

        // POST: api/Maps
        [HttpPost]
        [AppAuthorize(DemoPermissions.AddMap)]
        public async Task<IActionResult> PostMap([FromBody] MapModel.MapUpdateEdit map)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMap = new Map {Name = map.Name, MapTypeId = map.Type, Descriptions = map.Descriptions};
            try
            {
                _context.Maps.Add(newMap);

                await _context.SaveChangesAsync();

                var mapRoles = _context.MapRoles.Where(x => x.MapId == newMap.Id);

                _context.MapRoles.RemoveRange(mapRoles);

                await _context.SaveChangesAsync();

                foreach (var roleName in map.RolesAssigned)
                {
                    var role = _context.Role.SingleOrDefault(x => x.RoleName == roleName);
                    if (role != null)
                    {
                        _context.MapRoles.Add(new MapRole { MapId = newMap.Id, RoleId = role.Id });

                        await _context.SaveChangesAsync();
                    }
                }

                return CreatedAtAction("GetMaps", new { id = newMap.Id }, newMap);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
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