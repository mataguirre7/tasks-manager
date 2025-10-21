using API.Application;
using API.Application.Authentication;
using API.Definitions.Conventions;
using API.Definitions.Repositories;
using API.Domain.Extended;
using API.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Identity Configuration
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<TasksDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();

            // JWT Authentication Configuration
            var key = Encoding.ASCII.GetBytes("U{mVB9dEdhW1I=`JLU1ZK!S3jOgkel>A");
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization();
            services.AddControllers();

            services.AddDbContext<TasksDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Default")));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddAutoMapper(typeof(TasksAutoMapperProfile));

            services.AddControllersWithViews(options =>
            {
                options.Conventions.Add(new CustomControllerModelConvention());
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                    });
            });

            // JwtTokenService Configuration
            services.AddSingleton<JwtTokenService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var secretKey = configuration["Jwt:SecretKey"];
                return new JwtTokenService(secretKey!);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}