using DataLayer;
using DataLayer.Models;
using DataLayer.Models.Sensors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [Route("get_single/{packageID}/{sectionID}")]
        public Package Get(int packageID, int sectionID)
        {
            return PackageManager.GetPackage(packageID, sectionID);
        }

        /// <returns>All package, that is after <paramref name="lastPackageID"/>.</returns>
        [HttpGet]
        [Route("get_after/{lastPackageID}/{sectionID}")]
        public IEnumerable<Package> GetAfter(int lastPackageID, int sectionID)
        {
            return PackageManager.GetPackages(lastPackageID, sectionID);
        }

        [HttpGet]
        [Route("get_all/{sectionID}")]
        public IEnumerable<Package> GetAll(int sectionID)
        {
            return PackageManager.GetAllPackages(sectionID);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public HttpStatusCode Post([FromQuery] string packageJson)
        {
            try
            {
                dynamic json = JsonConvert.DeserializeObject(packageJson);

                var package = new Package()
                {
                    SectionID = json.sectionID,
                    SentTime = json.sentTime
                };

                for (int i = 0; i < json.speedValues.Count; i++)
                {
                    package.SpeedValues.Add(new Speed()
                    {
                        Value = json.speedValues[i].value,
                        SectionID = json.sectionID
                    });
                }

                for (int i = 0; i < json.timeValues.Count; i++)
                {
                    package.TimeValues.Add(new Time()
                    {
                        Value = json.timeValues[i].value,
                        SectionID = json.sectionID
                    });
                }

                for (int i = 0; i < json.yawValues.Count; i++)
                {
                    package.YawValues.Add(new Yaw()
                    {
                        Value = json.yawValues[i].value,
                        SectionID = json.sectionID
                    });
                }

                PackageManager.AddPackage(package);
            }
            catch (Exception e)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }
    }
}
