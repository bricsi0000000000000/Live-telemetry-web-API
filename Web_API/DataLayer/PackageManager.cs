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
                database.Package.Load();
                int newPackageID = database.Package.ToList().Any() ? database.Package.ToList().Last().ID + 1 : 1;

                database.Section.Load();
                var liveSection = database.Section.ToList().Where(x => x.IsLive);
                if (liveSection == null || !liveSection.Any())
                {
                    throw new Exception("There is no live section");
                }

                int sectionID = liveSection.FirstOrDefault().ID;


                database.Time.Load();
                foreach (var time in package.TimeValues)
                {
                    database.Time.Add(new Models.Sensors.Time()
                    {
                        SectionID = sectionID,
                        PackageID = newPackageID,
                        Value = time.Value
                    });
                }

                database.Speed.Load();
                foreach (var speed in package.SpeedValues)
                {
                    database.Speed.Add(new Models.Sensors.Speed()
                    {
                        SectionID = sectionID,
                        PackageID = newPackageID,
                        Value = speed.Value
                    });
                }

                database.Yaw.Load();
                foreach (var yaw in package.YawValues)
                {
                    database.Yaw.Add(new Models.Sensors.Yaw()
                    {
                        SectionID = sectionID,
                        PackageID = newPackageID,
                        Value = yaw.Value
                    });
                }

                database.Package.Add(new Package()
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
                database.Package.Load();

                var findPackage = database.Package.Where(x => x.ID == packageID && x.SectionID == sectionID).FirstOrDefault();
                if (findPackage == null)
                {
                    return null;
                }

                Package package = new Package()
                {
                    ID = packageID,
                    SentTime = findPackage.SentTime
                };

                database.Time.Load();
                foreach (var time in database.Time)
                {
                    if (time.PackageID == packageID)
                    {
                        package.TimeValues.Add(time);
                    }
                }

                database.Speed.Load();
                foreach (var speed in database.Speed)
                {
                    if (speed.PackageID == packageID)
                    {
                        package.SpeedValues.Add(speed);
                    }
                }

                database.Yaw.Load();
                foreach (var yaw in database.Yaw)
                {
                    if (yaw.PackageID == packageID)
                    {
                        package.YawValues.Add(yaw);
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
                database.Package.Load();
                var storedPackages = database.Package.Where(x => x.SectionID == sectionID).ToList();
                var packages = new List<Package>();

                foreach (var currentPackage in storedPackages)
                {
                    if (currentPackage.ID > lastSentPackageID)
                    {
                        Package package = new Package()
                        {
                            ID = currentPackage.ID
                        };

                        database.Time.Load();
                        foreach (var time in database.Time)
                        {
                            if (time.PackageID == currentPackage.ID)
                            {
                                package.TimeValues.Add(time);
                            }
                        }

                        database.Speed.Load();
                        foreach (var speed in database.Speed)
                        {
                            if (speed.PackageID == currentPackage.ID)
                            {
                                package.SpeedValues.Add(speed);
                            }
                        }

                        database.Yaw.Load();
                        foreach (var yaw in database.Yaw)
                        {
                            if (yaw.PackageID == currentPackage.ID)
                            {
                                package.YawValues.Add(yaw);
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
