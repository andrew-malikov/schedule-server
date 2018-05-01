using System;

namespace ScheduleServer.Configs {
    public abstract class BackgroundServiceConfig {
        protected TimeSpan interval;
        protected DateTime lastActionTime;

        public abstract TimeSpan Interval { get; set; }
        public abstract DateTime LastActionTime { get; set; }

        public bool IsActionTime(DateTime time) {
            return time - lastActionTime > interval;
        }
    }
}