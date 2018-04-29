using System;
using Microsoft.Extensions.Configuration;

namespace ScheduleServer.Configs {
    public class OsuApiJsonConfig : OsuApiConfig {

        public OsuApiJsonConfig(IConfiguration configs) {
            setUpConfig(configs);
        }

        private void setUpConfig(IConfiguration configs) {
            baseUri = configs.GetSection("OsuApi:BaseUri").Get<Uri>();
        }

        public override Uri GetBaseUri() {
            return baseUri;
        }
    }
}