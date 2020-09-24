using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FarmaciaDB.Models;

namespace FarmaciaDB.Data
{
    public class FaDBContext :DbContext
    {
        public FaDBContext(DbContextOptions <FaDBContext> options ) : base(options)
        {

        }

        public DbSet<Medicina> medicina { get; set; }
        public DbSet<cita> cita { get; set; }
        public DbSet<receta> receta { get; set; }

    }
}
