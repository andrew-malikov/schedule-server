using System;
using Microsoft.Extensions.Configuration;

namespace ScheduleServer.Configs {
    public class OsuApiJsonConfig : OsuApiConfig {

        public OsuApiJsonConfig(IConfiguration configs) {
            SetUpConfig(configs);
        }

        private void SetUpConfig(IConfiguration configs) {
            baseUri = configs.GetSection("OsuApi:BaseUri").Get<Uri>();
            scheduleUri = configs.GetSection("OsuApi:ScheduleUri").Get<Uri>();
        }

        public override Uri BaseUri {
            get { return baseUri; }
        }

        public override Uri ScheduleUri {
            get { return scheduleUri; }
        }
    }
}