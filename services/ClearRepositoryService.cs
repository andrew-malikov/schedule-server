using System;
using System.Threading;
using System.Threading.Tasks;

using ScheduleServer.Models;
using ScheduleServer.Repositories;
using ScheduleServer.Configs;
using ScheduleServer.Libs;

namespace ScheduleServer.Services {
    public class ClearRepositoryService : BackgroundService {

        protected SchedulesUpdate updater;
        protected ClearRepositoryServiceConfig config;

        public ClearRepositoryService(SchedulesUpdate updater, ClearRepositoryServiceConfig config) {
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