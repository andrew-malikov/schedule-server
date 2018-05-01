using System;
using System.Threading;
using System.Threading.Tasks;

using ScheduleServer.Models;
using ScheduleServer.Repositories;
using ScheduleServer.Configs;

namespace ScheduleServer.Services {
    public class ClearRepositoryService : BackgroundService {

        protected FileRepository<string, Schedule> repository;
        protected ClearRepositoryServiceConfig config;

        public ClearRepositoryService(FileRepository<string, Schedule> repository, ClearRepositoryServiceConfig config) {
            this.repository = repository;
            this.config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                if (config.IsActionTime()) {
                    config.LastActionTime = DateTime.Now;
                    repository.RemoveAll();
                }

                await Task.Delay(config.Interval, stoppingToken);
            }
        }
    }
}