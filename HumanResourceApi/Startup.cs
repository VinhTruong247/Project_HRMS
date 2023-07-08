
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HumanResourceApi;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace YourNamespace
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };

                });

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<HRMSContext>();
            services.AddScoped<UserRepo>();
            services.AddScoped<ExperienceRepo>();
            services.AddScoped<LeaveRepo>();
            services.AddScoped<JobRepo>();
            services.AddScoped<AllowanceRepo>();
            services.AddScoped<AttendanceRepo>();
            services.AddScoped<DepartmentRepo>();
            services.AddScoped<EmployeeContractRepo>();
            services.AddScoped<EmployeeRepo>();
            services.AddScoped<SkillRepo>();
            services.AddScoped<SkillEmployeeRepo>();
            services.AddScoped<DepartmentMemberRepo>();
            services.AddScoped<ProjectRepo>();
            services.AddScoped<EmployeeLoanLogRepo>();
            services.AddScoped<RoleRepo>();
            services.AddScoped<EmployeeBenefitRepo>();
            services.AddScoped<PaySlipRepo>();

            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HRMS API", Version = "v1" });
            //});
            services.AddSwaggerGen();
            services.AddSwaggerService();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
            });

            services.AddCors(o =>
            {
                o.AddPolicy("AllowAnyOrigin", corsPolicyBuilder =>
                {
                    corsPolicyBuilder
                        .SetIsOriginAllowed(x => _ = true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // auto migrate database
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<HRMSContext>();

                // Here is the migration executed
                dbContext.Database.Migrate();
            }

            app.UseCors("AllowAnyOrigin");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRMS API v1"));
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && reader.TryGetDateTime(out DateTime dateTime))
            {
                return dateTime;
            }


            return default;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("dd/MM/yyyy"));
        }
    }
}

