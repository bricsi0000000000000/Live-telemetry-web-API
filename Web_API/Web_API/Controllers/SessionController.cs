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
    public class SessionController : ControllerBase
    {
        /// <returns>Returns the ID of the active session.</returns>
        [HttpGet]
        [Route("live")]
        public Session Get()
        {
            return SessionManager.ActiveSession;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Session> GetAll()
        {
            return SessionManager.AllSessions;
        }

        /// <summary>
        /// Adds new Session.
        /// You onnly have to add the name and date
        /// </summary>
        /// <param name="session">New Session to add.</param>
        /// <response code="200">Sucessfully added.</response>
        /// <response code="409">There is already a Session with <paramref name="session"/>s name.</response>       
        /// <response code="500">There was an error adding <paramref name="session"/>.</response>       
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public HttpStatusCode Post([FromBody] Session session)
        {
            return SessionManager.AddSession(session);
        }

        /// <summary>
        /// Changes the live Session to the Session with <paramref name="sessionID"/> if there no live Session.
        /// </summary>
        /// <param name="sessionID">ID of the Session.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="409">There is already an active Session.</response>       
        /// <response code="500">There was an error with the database.</response>       
        [HttpPut]
        [Route("change_live")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public HttpStatusCode ChangeToLive([FromBody] int sessionID)
        {
            return SessionManager.ChangeActiveSession(sessionID, isLive: true);
        }

        /// <summary>
        /// Changes the Session with <paramref name="sessionID"/> to offline.
        /// </summary>
        /// <param name="sessionID">ID of the Session.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error with the database.</response>       
        [HttpPut]
        [Route("change_offline")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode ChangeToOffline([FromBody] int sessionID)
        {
            return SessionManager.ChangeActiveSession(sessionID, isLive: false);
        }

        /// <summary>
        /// Changes the Session's name.
        /// </summary>
        /// <param name="session">Session with new name.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error with the database.</response>       
        [HttpPut]
        [Route("change_name")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode ChangeName([FromBody] Session session)
        {
            return SessionManager.ChangeName(session);
        }

        /// <summary>
        /// Changes the Session's date.
        /// </summary>
        /// <param name="session">Session with new date.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error with the database.</response>       
        [HttpPut]
        [Route("change_date")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode ChangeDate([FromBody] Session session)
        {
            return SessionManager.ChangeDate(session);
        }

        /// <summary>
        /// Deletes the Session with <paramref name="sessionID"/>.
        /// </summary>
        /// <param name="sessionID">ID of the Session.</param>
        /// <response code="200">Sucessfully changed.</response>
        /// <response code="500">There was an error with the database.</response>       
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode Delete([FromQuery] int sessionID)
        {
            return SessionManager.Delete(sessionID);
        }
    }
}
