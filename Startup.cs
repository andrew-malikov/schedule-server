using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

using ScheduleServer.Models;
using ScheduleServer.Libs;
using ScheduleServer.Configs;
using ScheduleServer.Repositories;
using ScheduleServer.Converters;
using ScheduleServer.Clients;
using ScheduleServer.Services;
using ScheduleServer.Configuration;

namespace ScheduleServer {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var initConfig = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("settings/repository.json", optional: false, reloadOnChange: true)
                .AddJsonFile("settings/clients.json", optional: false, reloadOnChange: true)
                .AddJsonFile("settings/services.json", optional: false, reloadOnChange: true)
                .AddEntityFrameworkConfig(options => options.UseSqlite(initConfig.GetConnectionString("ConfigConnection")))
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

            services.AddTransient<JsonSerializator>();

            services.AddTransient<GroupSchedulesRepository>();
            services.AddTransient<TutorSchedulesRepository>();

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
            services.AddTransient<ScheduleHtmlConverter<GroupLessonHmtlConverter>>();
            services.AddTransient<ScheduleHtmlConverter<TutorLessonHmtlConverter>>();

            services.AddSingleton<OsuApiConfig, OsuApiJsonConfig>();
            services.AddSingleton<OsuFacultyApi>();
            services.AddSingleton<OsuCourseApi>();
            services.AddSingleton<OsuGroupApi>();
            services.AddSingleton<OsuDepartmentApi>();
            services.AddSingleton<OsuTutorApi>();

            services.AddSingleton<GroupScheduleManager>();
            services.AddSingleton<TutorScheduleManager>();

            services.AddTransient<OsuApi>();
            services.AddTransient<UniversityUpdate, SqliteUniversityUpdate>();
            services.AddTransient<SchedulesUpdate, SqliteScheduleUpdate>();

            services.AddSingleton<ClearRepositoryServiceConfig>();
            services.AddSingleton<IHostedService, ClearRepositoryService>();

            services.AddSingleton<UpdateUniversityDbServiceConfig>();
            services.AddSingleton<IHostedService, UpdateUniversityDbService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            var services = app.ApplicationServices;

            services.GetService<BasicDbSeeder<UniversityContext>>().Create();

            app.UseMvc();
        }
    }
}
