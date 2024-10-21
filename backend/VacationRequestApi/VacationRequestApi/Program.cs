using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VacationRequestApi.Data;
using VacationRequestApi.Data.Models;
using VacationRequestApi.MappingProfiles;
using VacationRequestApi.Services;

namespace VacationRequestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // Add services to the container.
            // Use InMemoryDatabase
            builder.Services.AddDbContext<VacationRequestContext>(options =>
                options.UseInMemoryDatabase("VacationRequestDb")); 


            builder.Services.AddControllers();
            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(VacationRequestProfile)); 
            // Register the service
            builder.Services.AddScoped<IVacationRequestService, VacationRequestService>(); 
            
            builder.Services.AddEndpointsApiExplorer();
            // Add Swagger services
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Vacation Request API",
                    Version = "v1",
                    Description = "A simple web app for submitting vacation requests"
                });

                // Enable XML comments for documentation
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Enable CORS
            app.UseCors("AllowAllOrigins");

            // Serve static files from wwwroot (React build)
            app.UseStaticFiles();

            // Enable routing
            app.UseRouting();

            // Seed initial data
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<VacationRequestContext>();
                dbContext.VacationRequests.Add(new VacationRequest
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(5),
                    VacationDays = 5,
                    Comment = "First vacation request"
                });
                dbContext.SaveChanges();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                // Enable middleware for Swagger
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vacation Request API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
