using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ScheduleServer.Configs {
    public class JsonFileRepositoryConfig : FileRepositoryConfig {
        protected string jsonRootPath;
        protected string jsonDirectoriesPath;
        protected IConfiguration externalConfig;

        public JsonFileRepositoryConfig(IConfiguration configs) {
            externalConfig = configs;

            jsonRootPath = "FileRepositoryConfig";
            jsonDirectoriesPath = $"{jsonRootPath}:Directories";

            setUpConfig(configs);
        }

        private void setUpConfig(IConfiguration configs) {
            directories = configs.GetSection(jsonDirectoriesPath).Get<Dictionary<string,string>>();
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
            externalConfig[jsonDirectoriesPath] = JsonConvert.SerializeObject(directories);
        }
    }
}