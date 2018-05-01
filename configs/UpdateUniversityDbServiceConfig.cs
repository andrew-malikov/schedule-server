using Microsoft.Extensions.Configuration;

namespace ScheduleServer.Configs {
    public class UpdateUniversityDbServiceConfig : BackgroundServiceJsonConfig {
        public UpdateUniversityDbServiceConfig(IConfiguration configs) : base(configs) { }

        protected override void InitJsonPaths() {
            jsonRootPath = "UpdateUniversityDbService";
            jsonIntervalPath = $"{jsonRootPath}:Interval";
            jsonLastActionTimePath = $"{jsonRootPath}:LastActionTime";
        }
    }
}