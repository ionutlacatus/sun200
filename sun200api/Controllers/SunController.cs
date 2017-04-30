using sun200api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;

namespace sun200api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SunController : ApiController
    {

        [HttpGet]
        [Route("sun")]
        public IHttpActionResult Get()
        {
            using (SunContext ctx = new SunContext())
            {
                SunSampleNew[] samples = ctx.Samples.ToArray();
                return Ok(samples);
            }
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("sun/max/{deviceid}")]
        public IHttpActionResult GetMax(int deviceid)
        {
            try
            {
                using (SunContext ctx = new SunContext())
                {
                    if (deviceid == 0)
                        return Ok(ctx.Samples.OrderByDescending(item => item.Id).First());
                    return Ok(ctx.Samples.Where(s => s.deviceid == deviceid).OrderByDescending(item => item.Id).First());
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [HttpGet]
        [Route("sun/gt/{id}/{deviceid}")]
        public IHttpActionResult GetGt(int id, int deviceid)
        {
            try
            {
                using (SunContext ctx = new SunContext())
                {
                    int maxSampleId = ctx.Samples.Where(s => s.Id >= id && s.deviceid == deviceid || deviceid == 0).Max(s => s.Id);
                    SunSampleNew sm = ctx.Samples.FirstOrDefault(s => s.Id == maxSampleId);
                    return Ok(sm);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [HttpGet]
        [Route("sun/{deviceid}")]
        public IHttpActionResult Get(int deviceid)
        {
            using (SunContext ctx = new SunContext())
            {
                SunSampleNew[] samples = ctx.Samples.Where(s => s.deviceid == deviceid).OrderByDescending(s => s.Id).ToArray();
                return Ok(samples);
            }
        }


        [HttpGet]
        [Route("sun/insert")]
        public IHttpActionResult Insert()
        {
            using (SunContext ctx = new SunContext())
            {
                ctx.Devices.Add(new SunDevice
                {
                    Id = 10,
                    Name = "Bucuresti",
                    Lat = 44.4379849,
                    Long = 25.9542103
                });

                ctx.Devices.Add(new SunDevice
                {
                    Id = 11,
                    Name = "Cluj",
                    Lat = 46.7834817,
                    Long = 23.546301
                });

                ctx.Devices.Add(new SunDevice
                {
                    Id = 12,
                    Name = "Brasov",
                    Lat = 45.6525766,
                    Long = 25.5262513
                });
                ctx.SaveChanges();
            }
            return Ok("done");
        }

        [Route("sun")]
        [HttpPost]
        public IHttpActionResult PostSunSample(SunSample payload)
        {

            if (payload.deviceid == 10)
                payload.deviceid = 1;
            if (payload.deviceid == 11)
                payload.deviceid = 2;
            if (payload.deviceid == 12)
                payload.deviceid = 3;
            try
            {
                SunSampleNew sample = new SunSampleNew
                {
                    Id = payload.Id,
                    deviceid = payload.deviceid,
                    voltage = Int32.Parse(payload.vals.Split(' ').Last()),
                    created = DateTime.UtcNow
                };


                using (SunContext ctx = new SunContext())
                {
                    ctx.Samples.Add(sample);
                    ctx.SaveChanges();
                }

                using (WebClient webClient = new WebClient())
                {
                    var address = "https://api.powerbi.com/beta/8c92d11f-39b7-4fac-b170-69a3ca72ecd4/datasets/a5e1d0f5-6a4e-469f-9172-0b63f3cc921f/rows?key=N1QwKreol%2B7GhqUsE%2B49ayvwGt%2FQrgs9laLOatw4ErPpSrYtlaOWnCKWsyQRbnfKZPjSrSKmsnuFJ7qL%2BxiLOQ%3D%3D";
                    webClient.UploadString(address,
                        $"[{{\"Id\" :{sample.Id},\"deviceID\" :{sample.deviceid}, \"voltage\" :{sample.voltage},\"DateTime\" :\"{DateTime.Now.ToString("yyyy - MM - ddTHH:mm: ssZ")}\" }}]");
                }

                return Ok(payload);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }
    }
}
