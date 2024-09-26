
using CodeSphere.Domain.Interfaces;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Context;
using CodeSphere.Infrastructure.Repos;
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

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidIssuer = builder.Configuration["Authentivation:Issuer"],
                    ValidAudience = builder.Configuration["Authentivation:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                                         builder.Configuration["Authentivation:SecretKey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true
                };
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
            builder.Services.AddScoped<IRepository<Bid>, BidRepository>();
            builder.Services.AddScoped<IRepository<PortfolioItem>, PortofolioItemRepository>();
            builder.Services.AddScoped<IRepository<Profile>, ProfileRepository>();
            builder.Services.AddScoped<IRepository<Project>, ProjectRepository>();
            builder.Services.AddScoped<IRepository<RequiredSkill>, RequiredSkillRepository>();
            builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();
            builder.Services.AddScoped<IRepository<Skill>, SkillRepository>();
            builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
            builder.Services.AddScoped<IRepository<Freelancer>, FreelancerRepository>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();

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


            app.MapControllers();

            app.Run();
        }
    }
}
