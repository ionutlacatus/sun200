using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sun200api.Models
{
    public class SunSample
    {
        public int Id { get; set; }
        public int deviceid { get; set; }
        public string vals { get; set; }
        public DateTime created { get; internal set; }
    }

    public class SunSampleNew
    {
        public int Id { get; set; }
        public int deviceid { get; set; }
        public int voltage { get; set; }
        public DateTime created { get; internal set; }
        [ForeignKey("deviceid")]
        public SunDevice Device { get; set; }
    }
}