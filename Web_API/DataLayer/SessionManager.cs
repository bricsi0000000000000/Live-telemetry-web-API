using DataLayer.Models;
using DataLayer.Models.Sensors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DataLayer
{
    public static class SessionManager
    {
        private static readonly string sensorNames = $"{nameof(Speed)};{nameof(Time)};{nameof(Yaw)}";

        /// <summary>
        /// Returns the active session ID.
        /// If there isn't any, it returns with -1.
        /// </summary>
        public static Session ActiveSession
        {
            get
            {
                try
                {
                    var database = new DatabaseContext();
                    database.Session.Load();
                    foreach (var session in database.Session)
                    {
                        if (session.IsLive)
                        {
                            return session;
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }


                return null;
            }
        }

        public static List<Session> AllSessions
        {
            get
            {
                var database = new DatabaseContext();
                database.Session.Load();
                return database.Session.ToList();
            }
        }

        /// <summary>
        /// Changes the Session's state with <paramref name="sessionID"/> to <paramref name="isLive"/>.
        /// </summary>
        public static HttpStatusCode ChangeActiveSession(int sessionID, bool isLive)
        {
            try
            {
                var database = new DatabaseContext();
                database.Session.Load();

                if (isLive)
                {
                    if (database.Session.ToList().FindAll(x => x.IsLive).Count > 0)
                    {
                        return HttpStatusCode.Conflict;
                    }
                }

                database.Session.ToList().Find(x => x.ID == sessionID).IsLive = isLive;
                database.SaveChanges();

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static HttpStatusCode AddSession(Session session)
        {
            try
            {
                var database = new DatabaseContext();
                database.Session.Load();

                if (database.Session.Where(x => x.Name.Equals(session.Name)).Any())
                {
                    return HttpStatusCode.Conflict;
                }

                var newSession = new Session()
                {
                    Name = session.Name,
                    Date = session.Date,
                    SensorNames = sensorNames
                };

                database.Session.Add(newSession);
                database.SaveChanges();

                ChangeActiveSession(newSession.ID, isLive: false);

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static HttpStatusCode ChangeName(Session session)
        {

            try
            {
                var database = new DatabaseContext();
                database.Session.Load();

                database.Session.ToList().Find(x => x.ID == session.ID).Name = session.Name;

                database.SaveChanges();

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static HttpStatusCode ChangeDate(Session session)
        {

            try
            {
                var database = new DatabaseContext();
                database.Session.Load();

                database.Session.ToList().Find(x => x.ID == session.ID).Date = session.Date;

                database.SaveChanges();

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static HttpStatusCode Delete(int sessionID)
        {

            try
            {
                var database = new DatabaseContext();
                database.Session.Load();

                var Session = database.Session.ToList().Find(x => x.ID == sessionID);
                if (Session != null)
                {
                    database.Session.Remove(Session);
                    database.SaveChanges();
                }

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
