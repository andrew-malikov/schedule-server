using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ScheduleServer.Configs {
    public class FileRepositoryJsonConfig : FileRepositoryConfig {
        protected string jsonRootPath;
        protected string jsonDirectoriesPath;
        protected IConfiguration externalConfig;

        public FileRepositoryJsonConfig(IConfiguration configs) {
            externalConfig = configs;

            jsonRootPath = "FileRepositoryConfig";
            jsonDirectoriesPath = $"{jsonRootPath}:Directories";

            SetUpConfig(configs);
        }

        private void SetUpConfig(IConfiguration configs) {
            directories = configs.GetSection(jsonDirectoriesPath).Get<Dictionary<string,string>>();
        }

        public override void AddDirectory(string key, string value) {
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