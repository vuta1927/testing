using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Demo.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Authorization;
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
    [AppAuthorize(DemoPermissions.ViewMap)]
    public class RoadsController : Controller
    {
        private readonly DemoContext _context;

        public RoadsController(DemoContext context)
        {
            _context = context;
        }

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

        // GET: api/Roads
        [HttpGet("From/{mapId}/{pos}")]
        public IActionResult GetRoads([FromRoute] int mapId, [FromRoute] int pos)
        {
            var data = _context.GoogleRoads.Where(r => r.MapId == mapId).Skip(pos).Take(11).ToList();
            if (data.Count < 0)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // GET: api/Roads/5
        [HttpGet("search/{text}/{mapId}")]
        public async Task<IActionResult> GetRoads([FromRoute] string text, [FromRoute] int mapId, [FromRoute] int pos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sqlText = new SqlParameter("text", text);
            var sqlMapId = new SqlParameter("mapId", mapId);
            var sqlPos = new SqlParameter("pos", pos);
            var roads = await _context.GoogleRoads.FromSql(@"SELECT * FROM [dbo].[GoogleRoads] WHERE FREETEXT ((Direction, Name), @text) AND MapId=@mapId ORDER BY Id", sqlText, sqlMapId).AsNoTracking().ToListAsync();

            if (roads.Count < 0)
            {
                return NotFound();
            }

            return Ok(roads);
        }

        // PUT: api/Roads/5
        [HttpPut("{id}")]
        [AppAuthorize(DemoPermissions.MapEdit)]
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
                var r = new GoogleRoad()
                {
                    Id = road.Id,
                    Name = road.Name,
                    Direction = road.Direction,
                    Distance = road.Distance,
                    Color = road.Color,
                    Paths = road.Paths,
                    MapId = road.MapId
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
                    var road = new GoogleRoad()
                    {
                        Id = data[i].Id,
                        Name = data[i].Name,
                        Direction = data[i].Direction,
                        Distance = data[i].Distance,
                        Color = data[i].Color,
                        Paths = data[i].Paths,
                        MapId = data[i].MapId
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
        [AppAuthorize(DemoPermissions.MapAdd)]
        public async Task<IActionResult> PostRoad([FromBody] RequestRoad road)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newRoad = new GoogleRoad()
            {
                Paths = road.Paths,
                Direction = road.Direction,
                Distance = road.Distance,
                Color = road.Color,
                Name = road.Name,
                MapId = road.MapId
            };
            try
            {
                _context.GoogleRoads.Add(newRoad);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRoad", new { id = newRoad.Id }, newRoad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Roads/5
        [HttpDelete("{id}")]
        [AppAuthorize(DemoPermissions.MapDelete)]
        public async Task<IActionResult> DeleteRoad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var iconOnRoad = await _context.GoogleRoadIcons.SingleOrDefaultAsync(i => i.GoogleRoadId == id);
            if (iconOnRoad != null)
            {
                _context.GoogleRoadIcons.Remove(iconOnRoad);
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