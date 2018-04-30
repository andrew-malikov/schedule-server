using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using ScheduleServer.Models;
using ScheduleServer.Libs;
using ScheduleServer.Configs;
using ScheduleServer.Repositories;
using ScheduleServer.Converters;
using ScheduleServer.Clients;

namespace ScheduleServer {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("settings/repository.json")
                .AddJsonFile("settings/clients.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddTransient<IConfiguration>(provider => Configuration);

            var connection = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            services.AddDbContext<UniversityContext>(options => options.UseSqlite(connection));

            services.AddTransient<BasicDbSeeder<UniversityContext>>();
            services.AddTransient<FileSystem>();

            services.AddTransient<ISerializable, JsonSerializator>();
            services.AddTransient<IGeneratable<object, string>, JsonNameGenerator>();

            services.AddSingleton<FileRepositoryConfig, FileRepositoryJsonConfig>();
            services.AddSingleton<FileRepository<string, Schedule>>();

            services.AddTransient<FacultyJsonConverter>();
            services.AddTransient<CourseJsonConverter>();
            services.AddTransient<GroupJsonConverter>();
            services.AddTransient<DepartmentJsonConverter>();
            services.AddTransient<TutorJsonConverter>();

            services.AddTransient<TimeHtmlConverter>();
            services.AddTransient<DisciplineHtmlConverter>();
            services.AddTransient<LessonTypeHtmlConverter>();
            services.AddTransient<GroupHtmlConverter>();
            services.AddTransient<TutorHtmlConverter>();
            services.AddTransient<RoomHtmlConverter>();
            services.AddTransient<GroupLessonHmtlConverter>();
            services.AddTransient<TutorLessonHmtlConverter>();
            services.AddTransient<DayHtmlConveter<GroupLessonHmtlConverter>>();
            services.AddTransient<DayHtmlConveter<TutorLessonHmtlConverter>>();
            services.AddTransient<GroupScheduleHtmlConverter>();
            services.AddTransient<TutorScheduleHtmlConverter>();

            services.AddSingleton<OsuApiConfig, OsuApiJsonConfig>();
            services.AddSingleton<OsuFacultyApi>();
            services.AddSingleton<OsuCourseApi>();
            services.AddSingleton<OsuGroupApi>();
            services.AddSingleton<OsuDepartmentApi>();
            services.AddSingleton<OsuTutorApi>();

            services.AddTransient<OsuApi>();
            services.AddTransient<UniversityUpdater, SqliteUniversityUpdater>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            var services = app.ApplicationServices;

            services.GetService<BasicDbSeeder<UniversityContext>>().Create();

            services.GetService<FileRepository<string, Schedule>>().SetRootDirectory("schedules");

            app.UseMvc();
        }
    }
}
