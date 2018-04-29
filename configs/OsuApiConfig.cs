using System;

namespace ScheduleServer.Configs {
    public abstract class OsuApiConfig {
        protected Uri baseUri;

        public abstract Uri GetBaseUri();
    }
}