using Microsoft.Extensions.Configuration;

namespace ScheduleServer.Configs {
    public class ClearRepositoryServiceConfig : BackgroundServiceJsonConfig {
        public ClearRepositoryServiceConfig(IConfiguration configs) : base(configs) { }

        protected override void InitJsonPaths() {
            jsonRootPath = "ClearRepositoryService";
            jsonIntervalPath = $"{jsonRootPath}:Interval";
            jsonLastActionTimePath = $"{jsonRootPath}:LastActionTime";
        }
    }
}