using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Context;
using CodeSphere.Infrastructure.Repos;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Freelancing_Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CodeSphereContext>(options =>
                                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            })
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = null;
             })
             .AddXmlSerializerFormatters();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                                         builder.Configuration["Authentication:SecretKey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true
                };
            });

            // 1. Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<CodeSphereContext>()
                .AddDefaultTokenProviders();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // use newtonsoftjson
            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters()
            .AddNewtonsoftJson();

            // add logger
            builder.Services.AddLogging();

            // Register Repositories
            builder.Services.AddScoped<IBidRepository, BidRepository>();
            builder.Services.AddScoped<IPortfolioItemRepository, PortfolioItemRepository>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IRequiredSkillRepository, RequiredSkillRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IFreelancerRepository, FreelancerRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Registere Services
            builder.Services.AddScoped<IBidService,BidService>();
            builder.Services.AddScoped<IClientService,ClientService>();
            builder.Services.AddScoped<IFreelancerService,FreelancerService>();
            builder.Services.AddScoped<IProfileService,ProfileService>();
            builder.Services.AddScoped<IProjectService,ProjectService>();
            builder.Services.AddScoped<IRequiredSkillService,RequiredSkillService>();
            builder.Services.AddScoped<IReviewService,ReviewService>();
            builder.Services.AddScoped<ISkillService,SkillService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();

            // Seed the database during application startup
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<CodeSphereContext>();

                try
                {
                    // Check if the database is empty and seed data if needed
                    SeedDatabase(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            app.Run();
        }

        // Method to seed data
        private static void SeedDatabase(CodeSphereContext context)
        {
            // Check if the database is already seeded
            if (!context.Clients.Any() && !context.Freelancers.Any())
            {
                // Seed Clients
                var client1 = new Client
                {
                    Name = "Client 1",
                    CompanyName = "Client Company 1",
                    ContactNumber = "1234567890",
                    Rating = 4.5,
                    Role = "Client"
                };

                var client2 = new Client
                {
                    Name = "Client 2",
                    CompanyName = "Client Company 2",
                    ContactNumber = "0987654321",
                    Rating = 4.0,
                    Role = "Client"
                };

                context.Clients.AddRange(client1, client2);

                // Seed Freelancers
                var freelancer1 = new Freelancer
                {
                    Name = "Freelancer 1",
                    Rating = 5.0,
                    Role = "Freelancer",
                    Hourlysalary = 20.5
                };

                var freelancer2 = new Freelancer
                {
                    Name = "Freelancer 2",
                    Rating = 4.2,
                    Role = "Freelancer",
                    Hourlysalary = 18.0
                };

                context.Freelancers.AddRange(freelancer1, freelancer2);

                // Seed Projects
                var project1 = new Project
                {
                    Title = "Project 1",
                    Description = "This is a description for Project 1",
                    Budget = 1000.0,
                    Status = "Open",
                    Client = client1
                };

                var project2 = new Project
                {
                    Title = "Project 2",
                    Description = "This is a description for Project 2",
                    Budget = 2000.0,
                    Status = "Open",
                    Client = client2
                };

                context.Projects.AddRange(project1, project2);

                // Seed Bids
                var bid1 = new Bid
                {
                    Amount = 500.0,
                    Proposal = "Proposal for Project 1",
                    Freelancer = freelancer1,
                    Project = project1
                };

                var bid2 = new Bid
                {
                    Amount = 1500.0,
                    Proposal = "Proposal for Project 2",
                    Freelancer = freelancer2,
                    Project = project2
                };

                context.Bids.AddRange(bid1, bid2);

                // Save changes to the database
                context.SaveChanges();
            }
        }
    }
}
