using Microsoft.Extensions.Configuration;

namespace ScheduleServer.Configs {
    public class ClearRepositoryServiceConfig : BackgroundServiceJsonConfig {
        public ClearRepositoryServiceConfig(IConfiguration configs) : base(configs) { }

        protected override void InitJsonPaths() {
            jsonRootPath = "ClearRepositoryService";
            jsonActionIntervalPath = $"{jsonRootPath}:ActionInterval";
            jsonCheckIntervalPath = $"{jsonRootPath}:CheckInterval";
            jsonLastActionTimePath = $"{jsonRootPath}:LastActionTime";
        }
    }
}