using System;

namespace ScheduleServer.Configs {
    public abstract class OsuApiConfig {
        protected Uri baseUri;
        protected Uri scheduleUri;

        public abstract Uri BaseUri { get; }
        public abstract Uri ScheduleUri { get; }
    }
}