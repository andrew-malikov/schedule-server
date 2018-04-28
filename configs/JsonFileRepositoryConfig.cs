using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ScheduleServer.Configs {
    public class JsonFileRepositoryConfig : FileRepositoryConfig {
        protected string jsonRootPath;
        protected string jsonDirecoriesPath;
        protected IConfiguration externalConfig;

        JsonFileRepositoryConfig(IConfiguration configs) {
            externalConfig = configs;

            jsonRootPath = "fileRepositoryConfig";
            jsonDirecoriesPath = $"{jsonRootPath}:directories";

            setUpConfig(configs);
        }

        private void setUpConfig(IConfiguration configs) {
            directories = JsonConvert.DeserializeObject<Dictionary<string, string>>(configs[jsonDirecoriesPath]);
        }

        public override void AddDiretory(string key, string value) {
            directories.Add(key, value);
            SaveConfig();
        }

        public override string GetDirectory(string key) {
            return directories[key];
        }

        public override void RemoveDirectory(string key) {
            directories.Remove(key);
            SaveConfig();
        }

        private void SaveConfig() {
            externalConfig[jsonDirecoriesPath] = JsonConvert.SerializeObject(directories);
        }
    }
}