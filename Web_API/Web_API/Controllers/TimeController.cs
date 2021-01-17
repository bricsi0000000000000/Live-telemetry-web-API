using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Web_API.Models;
using Web_API_data_access_layer;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase, IChannelController<Time, float>
    {
        /// <summary>
        /// Gets the list of the Time channel.
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     [
        ///         {
        ///             "value": 0.1,
        ///             "id": 1
        ///         },
        ///         {
        ///             "value": 2,
        ///             "id": 2
        ///         },
        ///         {
        ///             "value": 25,
        ///             "id": 3
        ///         }
        ///     ]
        /// </remarks>
        /// <returns>All Time data.</returns>
        [HttpGet]
        public IEnumerable<Time> Get()
        {
            var channelsContext = new ChannelsContext();
            channelsContext.Times.Load();
            return channelsContext.Times;
        }

        /// <summary>
        /// Gets a single Time based on <paramref name="id"/>.
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     {
        ///         "value": 2,
        ///         "id": 3
        ///     }
        /// </remarks>
        /// <param name="id">ID of the value.</param>
        /// <returns>A single Time.</returns>
        [HttpGet]
        [Route("{id}")]
        public Time Get(int id)
        {
            var channelsContext = new ChannelsContext();
            channelsContext.Times.Load();

            return channelsContext.Times.ToList().Find(x => x.ID == id);
        }

        /// <summary>
        /// Posts a new value into Times.
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     4.12
        /// </remarks>
        /// <param name="value">Value of the Time channel.</param>
        /// <returns>An Http status code.</returns>
        /// <response code="200">Sucessfully added <paramref name="value"/> into values.</response>
        /// <response code="500">There was an error adding the <paramref name="value"/> into values.</response>       
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode Post([FromBody] float value)
        {
            var channelsContext = new ChannelsContext();
            try
            {
                channelsContext.Times.Add(new Time { Value = value });
                channelsContext.SaveChanges();
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }
    }
}
