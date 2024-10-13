using Microsoft.EntityFrameworkCore;
using CodeSphere.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CodeSphere.Infrastructure.Context
{
    public class CodeSphereContext : IdentityDbContext<User, IdentityRole, string>
    {
        public CodeSphereContext(DbContextOptions<CodeSphereContext> options): base(options)
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
            //TPH it's kind to storage the data go search 
            modelBuilder.Entity<Freelancer>().ToTable("AspNetUsers");
            modelBuilder.Entity<Client>().ToTable("AspNetUsers");



            modelBuilder.Entity<User>()
                .ToTable("AspNetUsers");


            modelBuilder.Entity<Freelancer>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Freelancer>("Freelancer");


            modelBuilder.Entity<Client>()
                .HasBaseType<User>() 
                .HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client");


            modelBuilder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();


            modelBuilder.Entity<Skill>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(login => new { login.LoginProvider, login.ProviderKey });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(userRole => new { userRole.UserId, userRole.RoleId });

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(userToken => new { userToken.UserId, userToken.LoginProvider, userToken.Name });

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
