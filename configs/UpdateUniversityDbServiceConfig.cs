using Microsoft.Extensions.Configuration;

namespace ScheduleServer.Configs {
    public class UpdateUniversityDbServiceConfig : BackgroundServiceJsonConfig {
        public UpdateUniversityDbServiceConfig(IConfiguration configs) : base(configs) { }

        protected override void InitJsonPaths() {
            jsonRootPath = "UpdateUniversityDbService";
            jsonActionIntervalPath = $"{jsonRootPath}:ActionInterval";
            jsonCheckIntervalPath = $"{jsonRootPath}:CheckInterval";
            jsonLastActionTimePath = $"{jsonRootPath}:LastActionTime";
        }
    }
}