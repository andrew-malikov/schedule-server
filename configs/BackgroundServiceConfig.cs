using System;

namespace ScheduleServer.Configs {
    public abstract class BackgroundServiceConfig {
        protected TimeSpan actionInterval;
        protected TimeSpan checkInterval;
        protected DateTime lastActionTime;

        public abstract TimeSpan ActionInterval { get; set; }
        public abstract TimeSpan CheckInterval { get; set; }
        public abstract DateTime LastActionTime { get; set; }

        public bool IsActionTime(DateTime time) {
            return time - lastActionTime > actionInterval;
        }
    }
}