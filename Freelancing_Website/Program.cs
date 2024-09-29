using CodeSphere.Domain.Interfaces.Repos;
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

            app.Run();
        }
    }
}
