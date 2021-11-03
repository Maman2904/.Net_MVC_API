using System;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCoreMvcCrud.Models
{
    public class KendaraanContext:DbContext
    {
        public KendaraanContext(DbContextOptions<KendaraanContext> options):base(options)
        {
        }

        public DbSet<Kendaraan> Kendaraan { get; set; }
    }
}
