using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        /// <returns>Returns the ID of the active section.</returns>
        [HttpGet]
        [Route("live")]
        public Section Get()
        {
            return SectionManager.ActiveSection;
        }

        /// <summary>
        /// Adds new section.
        /// </summary>
        /// <param name="section">New section to add.</param>
        /// <response code="200">Sucessfully added.</response>
        /// <response code="500">There was an error adding <paramref name="section"/>.</response>       
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode Post([FromBody] Section section)
        {
            try
            {
                SectionManager.AddSection(section);
                SectionManager.ChangeActiveSection(section.ID);
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }

        /// <summary>
        /// Changes the live section to the section with <paramref name="sectionID"/>.
        /// </summary>
        /// <param name="sectionID">ID of the new active section.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error changeing the live section.</response>       
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode Put([FromBody] int sectionID)
        {
            try
            {
                SectionManager.ChangeActiveSection(sectionID);
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }
    }
}
