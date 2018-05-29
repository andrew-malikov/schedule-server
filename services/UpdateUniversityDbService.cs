using System;
using System.Threading;
using System.Threading.Tasks;

using ScheduleServer.Configs;
using ScheduleServer.Libs;

namespace ScheduleServer.Services {
    public class UpdateUniversityDbService : BackgroundService {
        protected UniversityUpdate updater;
        protected UpdateUniversityDbServiceConfig config;

        public UpdateUniversityDbService(UniversityUpdate updater, UpdateUniversityDbServiceConfig config) {
            this.updater = updater;
            this.config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                if (config.IsActionTime()) {
                    config.LastActionTime = DateTime.Now;
                    updater.Update();
                }

                await Task.Delay(config.CheckInterval, stoppingToken);
            }
        }
    }
}