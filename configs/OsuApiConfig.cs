using System;

namespace ScheduleServer.Configs {
    public abstract class OsuApiConfig {
        protected Uri baseUri;
        protected Uri scheduleUri;

        public abstract Uri GetBaseUri();
        public abstract Uri GetScheduleUri();
    }
}