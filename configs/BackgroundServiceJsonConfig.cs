using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ScheduleServer.Configs {
    public abstract class BackgroundServiceJsonConfig : BackgroundServiceConfig {
        protected string jsonRootPath;
        protected string jsonIntervalPath;
        protected string jsonLastActionTimePath;
        protected IConfiguration externalConfig;

        public BackgroundServiceJsonConfig(IConfiguration configs) {
            this.externalConfig = configs;

            InitJsonPaths();
            SetUpConfig(configs);
        }

        protected abstract void InitJsonPaths();

        private void SetUpConfig(IConfiguration configs) {
            var rawInterval = configs.GetSection(jsonIntervalPath).Get<string>();
            interval = TimeSpan.Parse(rawInterval);
            lastActionTime = configs.GetSection(jsonLastActionTimePath).Get<DateTime>();
        }

        public override TimeSpan Interval {
            get { return interval; }
            set {
                interval = value;
                SaveConfig();
            }
        }

        public override DateTime LastActionTime {
            get { return lastActionTime; }
            set {
                lastActionTime = value;
                SaveConfig();
            }
        }

        protected void SaveConfig() {
            externalConfig[jsonIntervalPath] = JsonConvert.SerializeObject(Interval);
            externalConfig[jsonLastActionTimePath] = JsonConvert.SerializeObject(LastActionTime);
        }

        public bool IsActionTime() {
            return base.IsActionTime(DateTime.Now);
        }
    }
}