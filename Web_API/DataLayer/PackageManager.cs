using DataLayer.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace DataLayer
{
    public static class PackageManager
    {
        public static void AddPackage(Package package)
        {
            try
            {
                var database = new DatabaseContext();

                database.Times.Load();
                int lastTimeID = database.Times.ToList().Count > 0 ? database.Times.ToList().Last().ID : -1;
                foreach (var time in package.Times)
                {
                    time.ID = ++lastTimeID;
                    time.PackageID = package.ID;
                    database.Times.Add(time);
                }

                database.Speeds.Load();
                int lastSpeedID = database.Speeds.ToList().Count > 0 ? database.Speeds.ToList().Last().ID : -1;
                foreach (var speed in package.Speeds)
                {
                    speed.ID = ++lastSpeedID;
                    speed.PackageID = package.ID;
                    database.Speeds.Add(speed);
                }

                database.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static Package GetPackage(int packageID)
        {
            try
            {
                Package package = new Package()
                {
                    ID = packageID
                };

                var database = new DatabaseContext();

                database.Times.Load();
                foreach (var time in database.Times)
                {
                    if (time.PackageID == packageID)
                    {
                        package.Times.Add(time);
                    }
                }

                database.Speeds.Load();
                foreach (var speed in database.Speeds)
                {
                    if (speed.PackageID == packageID)
                    {
                        package.Speeds.Add(speed);
                    }
                }

                return package;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
