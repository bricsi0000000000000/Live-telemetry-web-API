using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Web_API.Controllers
{
    public interface IChannelController<Channel, ValueType>
    {
        [HttpGet]
        public IEnumerable<Channel> Get();

        [HttpGet]
        [Route("{id}")]
        public Channel Get(int id);

        [HttpPost]
        public HttpStatusCode Post([FromBody] ValueType value);
    }
}
