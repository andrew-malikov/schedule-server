using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ScheduleServer.Configs {
    public abstract class BackgroundServiceJsonConfig : BackgroundServiceConfig {
        protected string jsonRootPath;
        protected string jsonActionIntervalPath;
        protected string jsonCheckIntervalPath;
        protected string jsonLastActionTimePath;
        protected IConfiguration externalConfig;

        public BackgroundServiceJsonConfig(IConfiguration configs) {
            this.externalConfig = configs;

            InitJsonPaths();
            SetUpConfig(configs);
        }

        protected abstract void InitJsonPaths();

        private void SetUpConfig(IConfiguration configs) {
            var rawActionInterval = configs.GetSection(jsonActionIntervalPath).Value;
            actionInterval = TimeSpan.Parse(rawActionInterval);
            var rawCheckInterval = configs.GetSection(jsonCheckIntervalPath).Value;
            checkInterval = TimeSpan.Parse(rawCheckInterval);
            lastActionTime = configs.GetSection(jsonLastActionTimePath).Get<DateTime>();
        }

        public override TimeSpan ActionInterval {
            get { return actionInterval; }
            set {
                actionInterval = value;
                SaveConfig();
            }
        }

        public override TimeSpan CheckInterval {
            get { return checkInterval; }
            set {
                checkInterval = value;
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
            externalConfig[jsonActionIntervalPath] = JsonConvert.SerializeObject(ActionInterval);
            externalConfig[jsonCheckIntervalPath] = JsonConvert.SerializeObject(CheckInterval);
            externalConfig[jsonLastActionTimePath] = JsonConvert.SerializeObject(LastActionTime);
        }

        public bool IsActionTime() {
            return base.IsActionTime(DateTime.Now);
        }
    }
}