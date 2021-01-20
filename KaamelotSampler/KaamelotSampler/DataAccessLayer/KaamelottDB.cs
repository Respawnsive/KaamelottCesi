using KaamelotSampler.Models;
using Microsoft.AppCenter.Crashes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace KaamelotSampler.DataAccessLayer
{
    public class KaamelottDB : DbContext
    {
        public DbSet<Saample> Saamples { get; set; }

        public KaamelottDB()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                string dbfilepath = Path.Combine(FileSystem.AppDataDirectory, "kaamdb.db");
                optionsBuilder.UseSqlite($"Data Source={dbfilepath}");
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
