using DataLayer;
using DataLayer.Models;
using DataLayer.Models.Sensors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Web_API.Controllers.Interfaces;

namespace Web_API.Controllers.SensorControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeedController : ControllerBase, IChannelController<Speed, int>
    {
        [HttpGet]
        public IEnumerable<Speed> Get()
        {
            var channelsContext = new DatabaseContext();
            channelsContext.Speeds.Load();
            return channelsContext.Speeds;
        }

        [HttpGet]
        [Route("{id}")]
        public Speed Get(int id)
        {
            var channelsContext = new DatabaseContext();
            channelsContext.Speeds.Load();

            return channelsContext.Speeds.ToList().Find(x => x.ID == id);
        }

        [HttpPost]
        public HttpStatusCode Post([FromBody] int value)
        {
            var channelsContext = new DatabaseContext();
            try
            {
                channelsContext.Speeds.Add(new Speed { Value = value });
                channelsContext.SaveChanges();
            }
            catch (Exception)
            {
                return HttpStatusCode.Conflict;
            }

            return HttpStatusCode.OK;
        }
    }
}
