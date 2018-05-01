using System;
using System.Threading;
using System.Threading.Tasks;

using ScheduleServer.Configs;
using ScheduleServer.Libs;

namespace ScheduleServer.Services {
    public class UpdateUniversityDbService : BackgroundService {
        protected UniversityUpdater updater;
        protected UpdateUniversityDbServiceConfig config;

        public UpdateUniversityDbService(UniversityUpdater updater, UpdateUniversityDbServiceConfig config) {
            this.updater = updater;
            this.config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                if (config.IsActionTime()) {
                    config.LastActionTime = DateTime.Now;
                    updater.FullUpdate();
                }

                await Task.Delay(config.Interval, stoppingToken);
            }
        }
    }
}