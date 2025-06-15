
using Microsoft.EntityFrameworkCore;
using online_course.core.Data;
using online_course.Data;
using online_course.service;

namespace online_course_api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // here in this section we have two parts 
            #region Service Configuration
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;



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

            #region Middleware Configuration
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
            #endregion
        }
    }
}
