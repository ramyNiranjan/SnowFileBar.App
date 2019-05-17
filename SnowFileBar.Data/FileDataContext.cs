using Microsoft.EntityFrameworkCore;
using SnowFileBar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFileBar.Data
{
    public class FileDataContext:DbContext
    {
        public FileDataContext(DbContextOptions<FileDataContext> options)
         : base(options)
        { }

        public DbSet<FileBarData> FilesData { get; set; }

        protected override void OnModelCreating
        (ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FileBarData>().HasData(
           new
           {
               Id = 1,
               ColourName = "Red",
               Size = 4


           });

        }
    }
}
