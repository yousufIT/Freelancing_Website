using CodeSphere.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodeSphere.Infrastructure.Context
{
    public class CodeSphereContext : DbContext
    {
        public CodeSphereContext(DbContextOptions<CodeSphereContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<RequiredSkill> RequiredSkills { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Review> Reviews { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasOne(u => u.Profile)
               .WithOne(p => p.User)
               .HasForeignKey<Profile>(p => p.UserId);


            modelBuilder.Entity<Profile>()
               .HasMany(p => p.Skills)
               .WithMany(s => s.Profiles);


            modelBuilder.Entity<Project>()
               .HasOne(p => p.Client)
               .WithMany(u => u.Projects)
               .HasForeignKey(p => p.ClientId);


            modelBuilder.Entity<Bid>()
               .HasOne(b => b.Freelancer)
               .WithMany(u => u.Bids)
               .HasForeignKey(b => b.FreelancerId);


           modelBuilder.Entity<Bid>()
                .HasOne(b => b.Project)
                .WithMany(p => p.Bids)
                .HasForeignKey(b => b.ProjectId);


            modelBuilder.Entity<Review>()
                .HasOne(r => r.Client)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(r => r.ClientId);


             modelBuilder.Entity<Review>()
                .HasOne(r => r.Freelancer)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(r => r.FreelancerId);


            modelBuilder.Entity<Project>()
                .HasMany(p => p.RequiredSkills)
                .WithOne(rs => rs.Project)
                .HasForeignKey(rs => rs.ProjectId);

        }
    }
}
