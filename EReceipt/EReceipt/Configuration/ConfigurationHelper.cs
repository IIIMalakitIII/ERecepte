using EReceipt.BLL.Interface;
using EReceipt.BLL.Services;
using EReceipt.DAL.Context;
using EReceipt.DAL.DataBuilders;
using EReceipt.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EReceipt.Configuration
{
    public static class ConfigurationHelper
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetSection("ConnectionStrings:EReceipt").Get<string>(),
                    m => m.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                // Password
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // User
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = null;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidIssuer = configuration["Auth:Issuer"],
                    ValidAudience = configuration["Auth:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Key"]))
                };
            });
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            /*services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyName.ForAllUsers, configure =>
                    configure.RequireClaim(CustomClaimName.Role, Role.Owner, Role.Developer, Role.Lead));

                options.AddPolicy(PolicyName.ForLead, configure =>
                    configure.RequireClaim(CustomClaimName.Role, Role.Lead));

                options.AddPolicy(PolicyName.ForLeadAndOwner, configure =>
                    configure.RequireClaim(CustomClaimName.Role, Role.Lead, Role.Owner));
            });*/
        }

        public static IServiceCollection ConfigureDiServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IConfidantService, ConfidantService>()
                .AddScoped<IDiseaseHistoryService, DiseaseHistoryService>()
                .AddScoped<IDoctorService, DoctorService>()
                .AddScoped<IManufacturerService, ManufacturerService>()
                .AddScoped<IMedicalInstitutionService, MedicalInstitutionService>()
                .AddScoped<IMedicamentCategoryService, MedicamentCategoryService>()
                .AddScoped<IMedicamentService, MedicamentService>()
                .AddScoped<IPatientService, PatientService>()
                .AddScoped<IPharmacyService, PharmacyService>()
                .AddScoped<IReceiptService, ReceiptService>()
                .AddScoped<IRecordService, RecordService>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RemoteWorkApi service"
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public static void MigrateDataBase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            new UserDataBuilder(userManager, roleManager).SetData();
        }
    }
}
