
using System.Text;
using Hangfire;
using Hangfire.PostgreSql;
using JWTAuthTemplate.Context;
using JWTAuthTemplate.Models.Identity;
using JWTAuthTemplate.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace JWTAuthTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration["JWT:Secret"] = Environment.GetEnvironmentVariable("JWT_SECRET") ?? builder.Configuration["JWT:Secret"];
            builder.Configuration["JWT:ValidAudience"] = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? builder.Configuration["JWT:ValidAudience"];
            builder.Configuration["JWT:ValidIssuer"] = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? builder.Configuration["JWT:ValidIssuer"];
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("DefaultConnection");

            //Add Postgres database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            using (var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseNpgsql(connectionString).Options)) {
                context.Database.Migrate();
            }

            //Add identity
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<ScrapingService>();
            
            //Set up JWT
            builder.Services.AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                //TODO: CHANGE THESE VALUES!!!
                //These settings are super insecure. DO NOT USE THESE IN PRODUCTION.
                .AddJwtBearer(opts =>
                {
                    opts.SaveToken = true;
                    opts.RequireHttpsMetadata = false;
                    opts.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
                    };
                });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMvc();

            builder.Services.AddHangfire(config => {
                config.UsePostgreSqlStorage(x => x.UseNpgsqlConnection(connectionString));
            });

            builder.Services.AddHangfireServer();

            builder.Services.AddHostedService<DailyUpdateService>();


            if (builder.Environment.IsDevelopment())
            {
                //Comment this out to avoid seed data
                SampleSeedData.SeedData(builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>());

                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new() { Title = "Roblaws.API", Version = "v1"});
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"

                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
                });
            }

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
    

            //app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}