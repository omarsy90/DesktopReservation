using DeskReservationAPI;
using DeskReservationAPI.Model;
using DeskReservationAPI.Repository;
using DeskReservationAPI.Utility;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DeskReservationAPITest
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {

        public  IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddUserSecrets("e3dfcccf-0cb3-423a-b302-e3e92e95c128")
                .AddEnvironmentVariables()
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            
            var Config = GetIConfigurationRoot(Directory.GetCurrentDirectory());
        
            string jwtKey = Config.GetSection("Jwt:Key").Get<string>();
            string jwtIssuer = Config.GetSection("Jwt:Issuer").Get<string>();
            string jwtAudience = Config.GetSection("Jwt:Audience").Get<string>();
            int jwtduration = Config.GetSection("Jwt:DurationOnday").Get<int>();

            JWTSetting jWTSetting = new JWTSetting()
            {
                Key = jwtKey,
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                DurationOnDays = jwtduration,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                SecurityAlgorithm = SecurityAlgorithms.HmacSha256,
            };

        

            TokenManager tokenGenerator = new TokenManager(jWTSetting);
         




            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDBContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<ApplicationDBContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                    }

                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }

                };
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddSingleton<TokenManager>(tokenGenerator);
                services.AddScoped<IDeskRepository, DeskRepository>();
                services.AddScoped<IOfficeRepository, OfficeRepository>();
                services.AddScoped<IEquipmentRepository, EquipmentRepository>();
                services.AddScoped<IFixReservationRepository, FixReservationRepository>();
                services.AddScoped<IDeskRepository,DeskRepository>();
                services.AddScoped<IFlexReservationRepository, FlexReservationRepository>();
                services.AddScoped<ICommentRepository, CommentRepository>();
                services.AddScoped<IAuthenticationService,AuthenticationService>();
               
            });
        }



    }
}
