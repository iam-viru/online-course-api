
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using online_course.core.Data;
using online_course.Data;
using online_course.service;
using Serilog;
using Serilog.Templates;

namespace online_course_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog with the settings
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();
            try
            {
                // here in this section we have two parts 
                #region Service Configuration
                var builder = WebApplication.CreateBuilder(args);
                var configuration = builder.Configuration;

                builder.Services.AddApplicationInsightsTelemetry();

                builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .WriteTo.Console(new ExpressionTemplate(
                    // Include trace and span ids when present.
                    "[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {@m}\n{@x}"))
                .WriteTo.ApplicationInsights(
                  services.GetRequiredService<TelemetryConfiguration>(),
                  TelemetryConverter.Traces));

                Log.Information("Starting the SmartLearnByKarthik API...");

                //DB configuration goes here    
                // here AddDbContextPool is used to create a pool of DbContext instances 
                // its better to use DbContext pooling for performance reasons
                //also providers options are used to configure the SQL Server connection 
                builder.Services.AddDbContextPool<OnlineCourseDbContext>(options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("DbContext"),
                    providerOptions =>
                    {
                        providerOptions.EnableRetryOnFailure(//this is used to enable retry on failure
                            maxRetryCount: 5,// maximum number of retries
                            maxRetryDelay: TimeSpan.FromSeconds(30),// maximum delay between retries
                            errorNumbersToAdd: null);
                    });
                    //options.EnableSensitiveDataLogging(); //this is not recommending in Production it only for during development phase.
                });

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // Registering the services and repositories
                //Type of services registre
                //1. Addscoped means when the request will hit the API, till the request is completed only one instance of the service will be created. e.g, if you want to create a single instance of the service for the lifetime of the request, you can use AddScoped
                //2. AddTransient means every time the request will hit the API, a new instance of the service will be created. e.g, if you want to create a new instance of the service every time the request is made, you can use AddTransient
                //3. AddSingleton means only one instance of the service will be created and it will be used for all the requests

                builder.Services.AddScoped<ICourseCategoryService, CourseCategoryService>();
                builder.Services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
                builder.Services.AddScoped<ICourseService, CourseService>();
                builder.Services.AddScoped<ICourseRepository, CourseRepository>();
                #endregion

                #region CORS Implementation
                var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
                });
                #endregion

                #region Middleware Configuration
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
                app.UseCors("CorsPolicy");
                app.UseAuthorization();


                app.MapControllers();

                app.Run();
                #endregion
            }
            catch (Exception ex)
            {

                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
           
        }
    }
}
