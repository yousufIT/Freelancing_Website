
using CodeSphere.Domain.Interfaces;
using CodeSphere.Domain.Models;
using CodeSphere.Infrastructure.Context;
using CodeSphere.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // use newtonsoftjson
            builder.Services.AddControllers().AddNewtonsoftJson();

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
            builder.Services.AddScoped<IRepository<User>, UserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
