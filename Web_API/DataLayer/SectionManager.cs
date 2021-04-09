using DataLayer.Models;
using DataLayer.Models.Sensors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public static class SectionManager
    {
        private static readonly string sensorNames = $"{nameof(Speed)};{nameof(Time)}";

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
                    database.Sections.Load();
                    foreach (var section in database.Sections)
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

        /// <summary>
        /// Changes the active section to <paramref name="sectionID"/>'s section.
        /// Changes all the other sections to offline.
        /// </summary>
        /// <param name="sectionID">ID of the new live section.</param>
        public static void ChangeActiveSection(int sectionID)
        {
            try
            {
                var database = new DatabaseContext();
                database.Sections.Load();
                foreach (var section in database.Sections)
                {
                    section.IsLive = section.ID == sectionID;
                }
                database.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Adds a new section.
        /// </summary>
        /// <param name="section">Section to add.</param>
        public static void AddSection(Section section)
        {
            try
            {
                var database = new DatabaseContext();
                database.Sections.Load();
                section.SensorNames = sensorNames;
                database.Sections.Add(section);
                database.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
