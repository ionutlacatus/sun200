using sun200api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace sun200api
{
    public class SunContext : DbContext
    {

        public SunContext() : base("SunConnStr")
        {

        }

        public DbSet<SunSampleNew> Samples { get; set; }
        public DbSet<SunDevice> Devices { get; set; }
    }
}