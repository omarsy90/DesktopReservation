
using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Drawing;
using DeskReservationAPI.Utility;
using DeskReservationAPI.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace DeskReservationAPI
{
    public partial  class Program
    {
        public static void Main(string[] args)
        {



            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer("Data Source=t-Test\\Inst1;Initial Catalog=BookingDesk;Persist Security Info=True;User ID=;Password=;TrustServerCertificate=True"));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IDeskRepository, DeskRepository>();
            builder.Services.AddScoped<IOfficeRepository,OfficeRepository>();
            builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            builder.Services.AddScoped<IFixReservationRepository, FixReservationRepository>();
            builder.Services.AddScoped<IFlexReservationRepository,FlexReservationRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
           
 

            // JWT Configuration

            string jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
            string jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            string jwtAudience = builder.Configuration.GetSection("Jwt:Audience").Get<string>();
            int jwtduration = builder.Configuration.GetSection("Jwt:DurationOnday").Get<int>();

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

            TokenManager tokenManager = new TokenManager(jWTSetting);
            builder.Services.AddSingleton< TokenManager>(tokenManager);



            // implement custom TokenManager in swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desk ReservationAPI",
                    Version = "v1"
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

           

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                
            }).AddJwtBearer(o =>
            {
                o.Audience = jWTSetting.Audience;
                o.ClaimsIssuer = jWTSetting.Issuer;
                o.RequireHttpsMetadata = true;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAlgorithms = new[] { jWTSetting.SecurityAlgorithm },
                    ValidateIssuerSigningKey = jWTSetting.ValidateIssuerSigningKey,
                    IssuerSigningKey = tokenManager._symmetricSecurityKey,
                    ValidateIssuer = jWTSetting.ValidateIssuer,
                    ValidateAudience = jWTSetting.ValidateAudience,
                    ClockSkew = jWTSetting.ClockSkew,
                    ValidIssuer = jWTSetting.Issuer,
                    ValidAudience = jWTSetting.Audience,
                  
                 
                    
                };
            });

            // finish


            // allow cors policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:8080") // Replace with your frontend domain
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            
        

        // finish

        var app = builder.Build();

            Console.WriteLine("envirment variable : "+app.Environment.EnvironmentName);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseCors("AllowSpecificOrigins");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapGet("/", () =>  "welcome to restfull api for desk reservation" );
             app.MapControllers();
              
           

            app.Run();
        }
    }


    public partial class Program { }
}
