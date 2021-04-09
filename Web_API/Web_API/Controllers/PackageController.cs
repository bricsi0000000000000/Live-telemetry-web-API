using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public Package Get(int id)
        {
            return PackageManager.GetPackage(id);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode Post([FromBody] Package package)
        {
            try
            {
                PackageManager.AddPackage(package);
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }
    }
}
