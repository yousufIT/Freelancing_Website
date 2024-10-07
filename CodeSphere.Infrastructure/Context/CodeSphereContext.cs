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
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Freelancer>()
                .HasMany(f => f.Bids)
                .WithOne(b => b.Freelancer)
                .HasForeignKey(b => b.FreelancerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.PostedProjects)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Freelancer)
                .WithOne(f => f.Profile)
                .HasForeignKey<Profile>(p => p.FreelancerId);

            modelBuilder.Entity<Freelancer>()
               .HasOne(f => f.Profile)
               .WithOne(p => p.Freelancer)
               .HasForeignKey<Profile>(p => p.FreelancerId);


            modelBuilder.Entity<Profile>()
               .HasMany(p => p.Skills);


            modelBuilder.Entity<Bid>()
               .HasOne(b => b.Freelancer)
               .WithMany(u => u.Bids)
               .HasForeignKey(b => b.FreelancerId)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Project)
                .WithMany(p => p.Bids)
                .HasForeignKey(b => b.ProjectId);


            modelBuilder.Entity<Review>()
                .HasOne(r => r.Client)
                .WithMany(c => c.ReviewsGiven)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Freelancer)
                .WithMany(f => f.ReviewsReceived)
                .HasForeignKey(r => r.FreelancerId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Project>()
                .HasMany(p => p.RequiredSkills);


           

        }
    }
}
