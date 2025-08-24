using CodeSphere.Domain.Interfaces.Repos;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Context;
using CodeSphere.Infrastructure.Repos;
using Freelancing_Website.Interfaces;
using Freelancing_Website.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Freelancing_Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CodeSphereContext>(options =>
                                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<JWTService>();

            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true; 
            })
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.PropertyNamingPolicy = null; 
                 })
                 .AddNewtonsoftJson(); 


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            // Identity (user/role management). Keep if you need UserManager/RoleManager.
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

            // Prevent cookie auth from redirecting to /Account/Login (useful fallback)
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
            });

            // NOW register Authentication and make JWT the default scheme (after AddIdentity)
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                        builder.Configuration["Authentication:SecretKey"] ?? string.Empty)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RoleClaimType = ClaimTypes.Role,   
                    NameClaimType = ClaimTypes.Email
                };
            });


            builder.Services.AddAuthorization();

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
            builder.Services.AddScoped<IDashboardService, DashboardService>();


            var app = builder.Build();

            // Adding Admin
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var logger = services.GetRequiredService<ILogger<Program>>();

                // Ensure Admin role exists
                var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
                if (!adminRoleExists)
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!roleResult.Succeeded)
                    {
                        logger.LogWarning("Failed to create Admin role: {Errors}", string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    }
                }

                // Create default admin user if none exists
                var adminEmail = builder.Configuration["Authentication:AdminEmail"] ?? "admin@example.com";
                var adminUsername = builder.Configuration["Authentication:AdminUserName"] ?? "admin";
                var adminPassword = builder.Configuration["Authentication:AdminPassword"] ?? "Admin@1234"; // change in production!

                var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
                if (existingAdmin == null)
                {
                    var adminUser = new User
                    {
                        UserName = adminUsername,
                        Email = adminEmail,
                        Name = "Administrator",
                        Role = "Admin",
                        Rating = 0
                    };

                    var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);
                    if (createAdminResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                        logger.LogInformation("Seeded default Admin user: {email}", adminEmail);
                    }
                    else
                    {
                        logger.LogWarning("Failed to create default admin: {Errors}", string.Join(", ", createAdminResult.Errors.Select(e => e.Description)));
                    }
                }
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


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
                };

                var freelancer2 = new Freelancer
                {
                    Name = "Freelancer 2",
                    Rating = 4.2,
                    Role = "Freelancer",
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

                var skill1 = new Skill
                {
                    Name = "C#",
                };
                var skill2 = new Skill
                {
                    Name = "C++",
                };
                var skill3 = new Skill
                {
                    Name = "Python",
                };
                var skill4 = new Skill
                {
                    Name = "JavaScript",
                };
                var skill5 = new Skill
                {
                    Name = "Java",
                };
                var skill6 = new Skill
                {
                    Name = "TypeScript",
                };
                var skill7 = new Skill
                {
                    Name = "Html",
                };
                var skill8 = new Skill
                {
                    Name = "Css",
                };
                var skill9 = new Skill
                {
                    Name = "Angular",
                };
                var skill10 = new Skill
                {
                    Name = "React",
                };
                var skill11 = new Skill
                {
                    Name = "Asp.net",
                };
                var skill12 = new Skill
                {
                    Name = "Laravel",
                };
                var skill13 = new Skill
                {
                    Name = "Node.js",
                };

                context.Skills.AddRange(skill1, skill2, skill3, skill4, skill5, skill6, skill7, skill8, skill9, skill10, skill11, skill12, skill13);


                // Seed Portfolio Items
                var portfolioItem1 = new PortfolioItem
                {
                    Title = "Book1",
                    Description = "book about programming",
                    ImageUrl = "https://cdn.pixabay.com/photo/2013/07/12/12/38/number-146030_1280.png"
                };
                var portfolioItem2 = new PortfolioItem
                {
                    Title = "Website1",
                    Description = "a good website for books",
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/11/03/08/46/number-1019717_640.jpg"
                };
                var portfolioItem3 = new PortfolioItem
                {
                    Title = "App1",
                    Description = "an app that show gold prices",
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/11/03/08/47/number-1019719_640.jpg"
                };
                var portfolioItem4 = new PortfolioItem
                {
                    Title = "Documentry",
                    Description = "a documentry about AI",
                    ImageUrl = "https://cdn.pixabay.com/photo/2013/07/12/12/37/number-146023_640.png"
                };

                context.PortfolioItems.AddRange(portfolioItem1,portfolioItem2,portfolioItem3,portfolioItem4 );


                // Seed Profiles
                var profile1 = new Profile
                {
                    Bio = "I am a good coder",
                    Freelancer = freelancer1,
                    Skills = new List<Skill>() { skill1, skill4, skill8, skill13 },
                    Portfolio = new List<PortfolioItem> { portfolioItem1, portfolioItem2}
                };

                var profile2 = new Profile
                {
                    Bio = "I am a good programmer :)",
                    Freelancer = freelancer2,
                    Skills = new List<Skill>() { skill2, skill4, skill7, skill10,skill11 },
                    Portfolio = new List<PortfolioItem> { portfolioItem3, portfolioItem4 }
                };

                context.Profiles.AddRange(profile1, profile2);

                freelancer1.Profile = profile1;

                freelancer2.Profile = profile2;


                // Save changes to the database
                try
                {
                    context.SaveChanges();
                    Console.WriteLine("Data saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

            }
        }
    }
}
