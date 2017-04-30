using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sun200api.Models
{
    public class SunDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}