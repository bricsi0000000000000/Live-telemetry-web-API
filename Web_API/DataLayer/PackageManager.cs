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
                database.Packages.Load();
                int newPackageID = database.Packages.ToList().Any() ? database.Packages.ToList().Last().ID + 1 : 1;

                database.Sections.Load();
                var liveSection = database.Sections.ToList().Where(x => x.IsLive);
                if (liveSection == null || !liveSection.Any())
                {
                    throw new Exception("There is no live section");
                }

                int sectionID = liveSection.FirstOrDefault().ID;


                database.Times.Load();
                int lastTimeID = database.Times.ToList().Any() ? database.Times.ToList().Last().ID : -1;
                foreach (var time in package.Times)
                {
                    database.Times.Add(new Models.Sensors.Time()
                    {
                        SectionID = sectionID,
                        PackageID = newPackageID,
                        Value = time.Value
                    });
                }

                database.Speeds.Load();
                int lastSpeedID = database.Speeds.ToList().Any() ? database.Speeds.ToList().Last().ID : -1;
                foreach (var speed in package.Speeds)
                {
                    database.Speeds.Add(new Models.Sensors.Speed()
                    {
                        SectionID = sectionID,
                        PackageID = newPackageID,
                        Value = speed.Value
                    });
                }

                database.Packages.Add(new Package()
                {
                    SectionID = sectionID,
                    SentTime = package.SentTime
                });

                database.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static Package GetPackage(int packageID, int sectionID)
        {
            try
            {
                var database = new DatabaseContext();
                database.Packages.Load();

                var findPackage = database.Packages.Where(x => x.ID == packageID && x.SectionID == sectionID).FirstOrDefault();
                if (findPackage == null)
                {
                    return null;
                }

                Package package = new Package()
                {
                    ID = packageID,
                    SentTime = findPackage.SentTime
                };

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

        public static List<Package> GetPackages(int lastSentPackageID, int sectionID)
        {
            try
            {
                var database = new DatabaseContext();
                database.Packages.Load();
                var storedPackages = database.Packages.Where(x => x.SectionID == sectionID).ToList();
                var packages = new List<Package>();

                foreach (var currentPackage in storedPackages)
                {
                    if (currentPackage.ID > lastSentPackageID)
                    {
                        Package package = new Package()
                        {
                            ID = currentPackage.ID
                        };

                        database.Times.Load();
                        foreach (var time in database.Times)
                        {
                            if (time.PackageID == currentPackage.ID)
                            {
                                package.Times.Add(time);
                            }
                        }

                        database.Speeds.Load();
                        foreach (var speed in database.Speeds)
                        {
                            if (speed.PackageID == currentPackage.ID)
                            {
                                package.Speeds.Add(speed);
                            }
                        }

                        packages.Add(package);
                    }
                }

                return packages;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static List<Package> GetAllPackages(int sectionID)
        {
            return GetPackages(0, sectionID);
        }
    }
}
