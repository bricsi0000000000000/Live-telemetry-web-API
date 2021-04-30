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
    public static class SectionManager
    {
        private static readonly string sensorNames = $"{nameof(Speed)};{nameof(Time)};{nameof(Yaw)}";

        /// <summary>
        /// Returns the active section ID.
        /// If there isn't any, it returns with -1.
        /// </summary>
        public static Section ActiveSection
        {
            get
            {
                try
                {
                    var database = new DatabaseContext();
                    database.Section.Load();
                    foreach (var section in database.Section)
                    {
                        if (section.IsLive)
                        {
                            return section;
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

        public static List<Section> AllSections
        {
            get
            {
                var database = new DatabaseContext();
                database.Section.Load();
                return database.Section.ToList();
            }
        }

        /// <summary>
        /// Changes the section's state with <paramref name="sectionID"/> to <paramref name="isLive"/>.
        /// </summary>
        public static HttpStatusCode ChangeActiveSection(int sectionID, bool isLive)
        {
            try
            {
                var database = new DatabaseContext();
                database.Section.Load();

                if (isLive)
                {
                    if (database.Section.ToList().FindAll(x => x.IsLive).Count > 0)
                    {
                        return HttpStatusCode.Conflict;
                    }
                }

                database.Section.ToList().Find(x => x.ID == sectionID).IsLive = isLive;
                database.SaveChanges();

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static HttpStatusCode AddSection(Section section)
        {
            try
            {
                var database = new DatabaseContext();
                database.Section.Load();

                var newSection = new Section()
                {
                    Name = section.Name,
                    Date = section.Date,
                    SensorNames = sensorNames
                };

                database.Section.Add(newSection);
                database.SaveChanges();

                ChangeActiveSection(newSection.ID, isLive: false);

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static HttpStatusCode ChangeName(Section section)
        {

            try
            {
                var database = new DatabaseContext();
                database.Section.Load();

                database.Section.ToList().Find(x => x.ID == section.ID).Name = section.Name;

                database.SaveChanges();

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static HttpStatusCode ChangeDate(Section section)
        {

            try
            {
                var database = new DatabaseContext();
                database.Section.Load();

                database.Section.ToList().Find(x => x.ID == section.ID).Date = section.Date;

                database.SaveChanges();

                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static HttpStatusCode Delete(int sectionID)
        {

            try
            {
                var database = new DatabaseContext();
                database.Section.Load();

                var section = database.Section.ToList().Find(x => x.ID == sectionID);
                if (section != null)
                {
                    database.Section.Remove(section);
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
