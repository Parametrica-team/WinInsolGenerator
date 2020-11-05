using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WinInsolGenerator;

namespace WinInsolGenerator
{
    class ApplicationContext: DbContext
    {
        //public DbSet<InsolationData> InsolationData_S_M_5 { get; set; }
        public DbSet<InsolationDataTest> InsolationDatas { get; set; }

        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=Max;Password=1122;");
        }
    }
}
