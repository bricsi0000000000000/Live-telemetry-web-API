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
    public class SectionController : ControllerBase
    {
        /// <returns>Returns the ID of the active section.</returns>
        [HttpGet]
        [Route("live")]
        public Section Get()
        {
            return SectionManager.ActiveSection;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Section> GetAll()
        {
            return SectionManager.AllSections;
        }

        /// <summary>
        /// Adds new section.
        /// You onnly have to add the name and date
        /// </summary>
        /// <param name="section">New section to add.</param>
        /// <response code="200">Sucessfully added.</response>
        /// <response code="500">There was an error adding <paramref name="section"/>.</response>       
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode Post([FromBody] Section section)
        {
            HttpStatusCode httpStatusCode;

            httpStatusCode = SectionManager.AddSection(section);
            if (httpStatusCode == HttpStatusCode.OK)
            {
                httpStatusCode = SectionManager.ChangeActiveSection(section.ID, isLive: false);
            }

            return httpStatusCode;
        }

        /// <summary>
        /// Changes the live section to the section with <paramref name="sectionID"/> if there no live section.
        /// </summary>
        /// <param name="sectionID">ID of the section.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error with the database.</response>       
        /// <response code="409">There is already an active section.</response>       
        [HttpPut]
        [Route("change_live")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public HttpStatusCode ChangeToLive([FromBody] int sectionID)
        {
            return SectionManager.ChangeActiveSection(sectionID, isLive: true);
        }

        /// <summary>
        /// Changes the section with <paramref name="sectionID"/> to offline.
        /// </summary>
        /// <param name="sectionID">ID of the section.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error with the database.</response>       
        [HttpPut]
        [Route("change_offline")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode ChangeToOffline([FromBody] int sectionID)
        {
            return SectionManager.ChangeActiveSection(sectionID, isLive: false);
        }

        /// <summary>
        /// Changes the section's name.
        /// </summary>
        /// <param name="section">Section with new name.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error with the database.</response>       
        [HttpPut]
        [Route("change_name")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode ChangeName([FromBody] Section section)
        {
            return SectionManager.ChangeName(section);
        }

        /// <summary>
        /// Changes the section's date.
        /// </summary>
        /// <param name="section">Section with new date.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error with the database.</response>       
        [HttpPut]
        [Route("change_date")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode ChangeDate([FromBody] Section section)
        {
            return SectionManager.ChangeDate(section);
        }

        /// <summary>
        /// Deletes the section with <paramref name="sectionID"/>.
        /// </summary>
        /// <param name="sectionID">ID of the section.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error with the database.</response>       
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode Delete([FromQuery] int sectionID)
        {
            return SectionManager.Delete(sectionID);
        }
    }
}
