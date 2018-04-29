using System;
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

namespace ScheduleServer {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("settings/repository.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
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

            services.AddSingleton<FileRepositoryConfig, JsonFileRepositoryConfig>();
            services.AddSingleton<FileRepository<string, Schedule>>();

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
