using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Model;
namespace Web.Controllers.map
{
    [Produces("application/json")]
    [Route("api/Map")]
    public class MapController : Controller
    {
        private readonly DemoContext _ctx;
        private readonly ILogger<MapController> _logger;

        public MapController(DemoContext context, ILogger<MapController> iLogger)
        {
            this._ctx = context;
            this._logger = iLogger;
        }

        [HttpGet]
        public IActionResult GetAllMap()
        {
            try
            {
                var maps = _ctx.Maps.ToList();
                return Ok(maps);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }

        public class MapType
        {
            public int Google = 100;
            public int Microsoft = 200;
            public int Fabric = 300;
        }
    }
}