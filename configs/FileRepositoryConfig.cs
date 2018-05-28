using System.Collections.Generic;

namespace ScheduleServer.Configs {
    public abstract class FileRepositoryConfig {
        protected Dictionary<string, string> directories;

        public abstract void AddDirectory(string key, string value);
        public abstract string GetDirectory(string key);
        public abstract void RemoveDirectory(string key);
    }
}