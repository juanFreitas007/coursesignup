using Juan.CourseSignup.ApplicationCore.CommandHandlers;
using Juan.CourseSignup.ApplicationCore.Commands;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using Juan.CourseSignup.ApplicationCore.Services;
using Juan.CourseSignup.Infrastructure.Data;
using Juan.CourseSignup.Infrastructure.Logging;
using Juan.CourseSignup.Infrastructure.Services;
using Juan.CourseSignup.WebApi.Filters;
using Juan.CourseSignup.WebApi.Interfaces;
using Juan.CourseSignup.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Juan.CourseSignup.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {           
            // add in memory databases
            services.AddDbContext<CoursesDbContext>(c => c.UseInMemoryDatabase("CoursesSignup"));

            // register dependencies
            services.AddScoped(typeof(IRepository<>), typeof(EfGenericRepository<>));

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseStudentSignupService, CourseStudentSignupService>();
            services.AddScoped<ICourseViewModelService, CourseViewModelService>();
            services.AddScoped<ICommandRequestHandler<SignupStudentCourseCommand>, SingupStudentCourseCommandHandler>();
            services.AddScoped<IQueueSender<SignupStudentCourseCommand>, AzureQueueSender<SignupStudentCourseCommand>>();
            services.AddSingleton<IQueueReceiver<SignupStudentCourseCommand>, AzureQueueReceiver<SignupStudentCourseCommand>>();

            services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));

            services.AddHostedService<TimedHostedService>();

            services.AddMvc(config => config.Filters.Add(typeof(ApiExceptionFilter))).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseFileServer();

            //app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });

        
        }
    }
}
