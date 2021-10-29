using TechJobsPersistent.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace TechJobsPersistent.Data
{
    public class JobDbContext : DbContext
    {
        public JobDbContext()
        {

        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public ConnectionString ConnectionString { get; set; }

        public JobDbContext(ConnectionString connectionString)
        {
            ConnectionString = connectionString;
        }

        public JobDbContext(DbContextOptions<JobDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobSkill>()
                .HasKey(j => new { j.JobId, j.SkillId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySql(connectionString: ConnectionString.Value,
                new MySqlServerVersion(new Version(8, 0, 26)));
        }
    }
}
